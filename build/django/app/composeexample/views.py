from django.shortcuts import render
from django.http import HttpResponse

def index(request):
    html = "<!DOCTYPE html><html lang='en'><head><title>Django App</title></head><div>"
    html += "<h1>HTTP Headers</h1>"
    for key, value in request.headers.items():
        html += "<p><span style='font-weight: bold;'>" + str(key) + ": </span>" + str(value) + "</p>"
    html += "</div><html>"
    return HttpResponse(html)
