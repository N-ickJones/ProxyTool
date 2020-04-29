FROM ruby:2.7

RUN bundle config unset frozen

WORKDIR /usr/src/app

COPY ./app/Gemfile ./app/Gemfile.lock ./
RUN bundle install

COPY ./app/ .


