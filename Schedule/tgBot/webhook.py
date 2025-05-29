from fastapi import FastAPI, Request
from tgBot import bot, user_sessions

app = FastAPI()

@app.post("/notify")
async def notify(request: Request):
    data = await request.json()
    login = data.get("login")
    text = data.get("text")

    for tg_id, info in user_sessions.items():
        if info["login"] == login:
            await bot.send_message(chat_id=tg_id, text=text)
            return {"status": "sent"}

    return {"status": "user not found"}
