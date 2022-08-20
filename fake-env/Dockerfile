FROM python:3.7-alpine

WORKDIR /app

COPY fake-env/ /app/

RUN pip install -r requirements.txt

CMD [ "python", "./main.py" ]