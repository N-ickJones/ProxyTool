defmodule HelloWeb.PageController do
  use HelloWeb, :controller

  def index(conn, _params) do
    header = (conn.req_headers)
    render(conn, "index.html", header:  header)
  end
end
