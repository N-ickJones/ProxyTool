# Server Version

import flask
application = flask.Flask(__name__)

@application.route("/")
def index():
    html = "<!DOCTYPE html><html lang='en'><head><title>Flask App</title></head><div>"
    html += "<h1>HTTP Headers</h1>"
    for key, value in flask.request.headers:
        html += "<p><span style='font-weight: bold;'>" + str(key) + ": </span>" + str(value) + "</p>"
    html += "</div><html>"
    return html

#application.run(debug=True, host='0.0.0.0', port='8003')
