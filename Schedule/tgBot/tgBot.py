from aiogram import Bot, Dispatcher, types
from aiogram.types import ReplyKeyboardMarkup, KeyboardButton
from aiogram.fsm.storage.memory import MemoryStorage
from aiogram.fsm.context import FSMContext
from aiogram.fsm.state import State, StatesGroup

API_TOKEN = "7874153058:AAHc-c6NcpAFZ8SIv4sEX3jPLcAZftMkqcI"
bot = Bot(token=API_TOKEN)
dp = Dispatcher(storage=MemoryStorage())

# Хранилище Telegram ID ↔ логин
user_sessions = {}

# Авторизация
class AuthState(StatesGroup):
    login = State()
    password = State()

@dp.message(commands="start")
async def start(message: types.Message, state: FSMContext):
    await state.set_state(AuthState.login)
    await message.answer("Введите логин:")

@dp.message(AuthState.login)
async def enter_login(message: types.Message, state: FSMContext):
    await state.update_data(login=message.text)
    await state.set_state(AuthState.password)
    await message.answer("Введите пароль:")

@dp.message(AuthState.password)
async def enter_password(message: types.Message, state: FSMContext):
    data = await state.get_data()
    login, password = data['login'], message.text

    # Проверка авторизации
    import requests
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
    await state.clear()

