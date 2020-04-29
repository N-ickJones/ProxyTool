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

