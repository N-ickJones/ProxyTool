
#### Docker Compose Example to build Docker Network
    version: '3'

    services:
      nginx:
        build: ./build/nginx
        container_name: nginx
        hostname: nginx
        ports:
          - 80:80
          - 443:443
        # volumes:
        #- ./build/nginx/etc:/etc
        extra_hosts:
          - "*.localhost:127.0.0.1"
        depends_on:
          - django
          - flask
          - ruby

      db:
        image: postgres
        container_name: postgres
        environment:
          - POSTGRES_USER=postgres
          - POSTGRES_PASSWORD=password
        restart: always

      react:
        build: ./build/react
        container_name: react
        hostname: react
        # command: bash -c "cd ProxyWorldClient && npm start" #Development
        command: dotnet out/ProxyWorldApi.dll   #Production
        ports:
          -  8000:8000
        volumes:
          - ./build/react/app/ProxyWorldClient/src:/app/ProxyWorldClient/src 

      razor:
        build: ./build/razor
        container_name: razor
        hostname: razor
        # command: dotnet watch run           # Development
        command: dotnet out/razor.dll       # Production
        ports:
          - 8001:8001
        volumes:
         - ./build/razor/app/Pages:/app/Pages

      django:
        build: ./build/django
        container_name: django
        hostname: django
        # command: python manage.py runserver 0.0.0.0:8002 # Development
        command: gunicorn --bind 0.0.0.0:8002 composeexample.wsgi:application   # Production 
        ports:
          - 8002:8002
        volumes:
          - ./build/django/app:/code
        depends_on:
          - db

      flask:
        build: ./build/flask
        container_name: flask
        hostname: flask
        # command: python app.py   # Development
        command: gunicorn --bind 0.0.0.0:8003 index:application  # Production 
        ports:
          - 8003:8003
        volumes:
          - ./build/flask/app:/code

      ruby:
        build: ./build/ruby
        container_name: ruby
        hostname: ruby
        # command: bundle exec rails server -b 0.0.0.0 -p 8004  # Development
        command: unicorn -p 8004  # Production
        ports:
          - 8004:8004

      spring:
        build: ./build/spring
        container_name: spring
        hostname: spring
        command: java -jar app.jar
        ports:
          - 8005:8005

      laravel:
        build: ./build/laravel
        container_name: laravel
        hostname: laravel
        command: php artisan serve --host=0.0.0.0 --port=8006
        ports: 
          - 8006:8006

      phoenix:
        build: ./build/phoenix
        container_name: phoenix
        hostname: phoenix
        command: bash -c "mix ecto.create && mix ecto.migrate && mix phx.server"
        ports:
          - 8007:8007
        environment:
          PGUSER: postgres
          PGPASSWORD: password
          PGDATABASE: phoenix
          PGPORT: 5432
          PGHOST: db
        depends_on:
          - db

      express:
        build: ./build/express
        container_name: express
        hostname: express
        command: node app.js
        ports:
          - 8008:8008
          
#### Dockerfile Examples used to build images
#### React
    # Dotnet
    FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS buildapi
    WORKDIR /app
    COPY app/ProxyWorldApi.csproj .
    RUN dotnet restore -r linux-musl-x64
    COPY app/. .
    RUN dotnet publish -c Release -o out

    # React
    RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
    RUN apt-get install -y nodejs
    WORKDIR /app/ProxyWorldClient
    RUN npm install --silent
    RUN npm install react-scripts@3.0.1 -g --silent
    RUN npm run build --silent
    WORKDIR /app


#### Razor
    FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
    WORKDIR /app

    COPY app/razor.csproj .
    RUN dotnet restore -r linux-musl-x64

    COPY app/. .
    RUN dotnet publish -c Release -o out

#### Django
    FROM python:3
    ENV PYTHONUNBUFFERED 1
    RUN mkdir /code
    WORKDIR /code
    COPY ./app/requirements.txt /code/
    RUN pip install -r requirements.txt
    COPY ./app/ /code/

#### Flask
    FROM python:3
    ENV PYTHONUNBUFFERED 1
    RUN mkdir /code
    WORKDIR /code
    COPY ./app/requirements.txt /code/
    RUN pip install -r requirements.txt
    COPY ./app/ /code/

#### Ruby
    FROM ruby:2.7

    RUN bundle config unset frozen

    WORKDIR /usr/src/app

    COPY ./app/Gemfile ./app/Gemfile.lock ./
    RUN bundle install

    COPY ./app/ .

#### Laravel
    FROM php:7
    RUN apt-get update -y && apt-get install -y openssl zip unzip git
    RUN curl -sS https://getcomposer.org/installer | php -- --install-dir=/usr/local/bin --filename=composer
    RUN docker-php-ext-install pdo

    WORKDIR /app
    COPY ./app/ /app
    RUN composer install

#### Spring
    FROM gradle AS build
    COPY --chown=gradle:gradle ./app/ /home/gradle/src
    WORKDIR /home/gradle/src
    RUN gradle build --no-daemon 

    FROM openjdk:8-jre-slim

    EXPOSE 8080

    RUN mkdir /app

    COPY --from=build /home/gradle/src/build/libs/*.jar /app/app.jar

    WORKDIR /app

#### Phoenix
    FROM elixir

    RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
    RUN apt-get install -y nodejs

    WORKDIR /app
    COPY ./app/ .
    RUN mix local.hex --force
    # --only prod # for production only
    RUN mix local.rebar --force
    RUN mix deps.get
    RUN mix deps.compile
    RUN mix compile

    WORKDIR /app/assets
    RUN npm install --silent
    WORKDIR /app

#### Express
    FROM node
    WORKDIR /app
    COPY ./app/package*.json ./
    RUN npm install --silent
    COPY ./app/ ./
 
