from aiogram import Bot, Dispatcher, Router, types, F
from aiogram.fsm.context import FSMContext
from aiogram.fsm.state import State, StatesGroup
from aiogram.fsm.storage.memory import MemoryStorage
from aiogram.types import Message
import requests

API_TOKEN = "7874153058:AAHc-c6NcpAFZ8SIv4sEX3jPLcAZftMkqcI"

bot = Bot(token=API_TOKEN)
dp = Dispatcher(storage=MemoryStorage())
router = Router()
dp.include_router(router)

# Telegram ID ↔ логин + SID
user_sessions = {}

# Состояния для FSM авторизации
class AuthState(StatesGroup):
    login = State()
    password = State()

# Команда /start
@router.message(F.text == "/start")
async def cmd_start(message: Message, state: FSMContext):
    await state.set_state(AuthState.login)
    await message.answer("Введите логин:")

# Ввод логина
@router.message(AuthState.login)
async def enter_login(message: Message, state: FSMContext):
    await state.update_data(login=message.text)
    await state.set_state(AuthState.password)
    await message.answer("Введите пароль:")

# Ввод пароля и авторизация
@router.message(AuthState.password)
async def enter_password(message: Message, state: FSMContext):
    data = await state.get_data()
    login, password = data["login"], message.text

    try:
        response = requests.post("http://localhost:5270/api/auth/login", json={
            "login": login,
            "password": password
        })

        if response.status_code == 200:
            sid = response.json()["sid"]
            user_sessions[message.from_user.id] = {"login": login, "sid": sid}
            await message.answer("✅ Авторизация прошла успешно")
        else:
            await message.answer("❌ Неверный логин или пароль")

    except requests.exceptions.RequestException as e:
        await message.answer(f"Ошибка соединения с API: {e}")

    await state.clear()
