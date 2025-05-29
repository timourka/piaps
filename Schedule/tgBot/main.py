import uvicorn
from tgBot import dp, bot
from aiogram import executor
import webhook

if __name__ == "__main__":
    import threading

    def run_webhook():
        uvicorn.run("webhook:app", host="0.0.0.0", port=8000)

    threading.Thread(target=run_webhook).start()
    executor.start_polling(dp, skip_updates=True)
