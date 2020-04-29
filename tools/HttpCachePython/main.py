import requests
import importlib
import sys

URL = "https://django.myvenv.club"
TEMP = ""
SCRIPT = "\u1f60"
PARAMS = {}
HEADERS = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:73.0) Gecko/20100101 Firefox/73.0",
    "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
    "Accept-Language": "en-US,en;q=0.5",
    "Content-Type": "application/json; charset=utf-8",
    "Accept-Encoding": f"gzip, deflate, {SCRIPT}",
    "Connection": "close",
    "Upgrade-Insecure-Requests": "1",
}

print(SCRIPT)
response = requests.get(url=URL, params=PARAMS, headers=HEADERS)
data = response.content
print(data)
