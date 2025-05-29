import asyncio
import uvicorn
from tgBot import bot, dp
from webhook import app as fastapi_app
from fastapi import FastAPI

async def start_fastapi():
    config = uvicorn.Config(fastapi_app, host="0.0.0.0", port=8000, log_level="info")
    server = uvicorn.Server(config)
    await server.serve()

async def start_bot():
    await dp.start_polling(bot)

async def main():
    await asyncio.gather(start_fastapi(), start_bot())

if __name__ == "__main__":
    asyncio.run(main())
