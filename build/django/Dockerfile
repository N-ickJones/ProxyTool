FROM python:3
ENV PYTHONUNBUFFERED 1
RUN mkdir /code
WORKDIR /code
COPY ./app/requirements.txt /code/
RUN pip install -r requirements.txt
COPY ./app/ /code/
EXPOSE 8002
