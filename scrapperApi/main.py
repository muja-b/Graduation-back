import re
import random

import requests
from bs4 import BeautifulSoup
from flask import Flask, request, render_template, json

app = Flask(__name__)


@app.route('/ScrapData', methods=['GET'])
def scrap():
    if request.method == 'GET':
        websites = ["https://ar.wikipedia.org/wiki/%D9%83%D8%AA%D8%A7%D8%A8%D8%A9",
                    "https://ar.wikipedia.org/wiki/%D8%AB%D9%82%D8%A7%D9%81%D8%A9_%D8%B9%D8%B1%D8%A8%D9%8A%D8%A9"]

        response = requests.get(
            url=random.choice(websites)
        )
        soup = BeautifulSoup(response.content, 'html.parser')
        texts = soup.find_all("p")
        while True:
            rand_idx = random.choice(texts)
            if len(rand_idx) > 10:
                text = rand_idx
                break

        text = text.get_text()
        text = ' '.join(re.findall(r'[\u0600-\u06FF]+', text))
        res = {
            "text": text
        }
        return res


if __name__ == '__main__':
    app.run(debug=True)