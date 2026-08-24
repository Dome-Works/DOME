"""Add healthcheck for Sockets and "live" status bubble on Socket page (fix for issue #48)"""

from flask import Flask, jsonify
from socket import socket as Socket, AF_INET, SOCK_STREAM

class SocketHealthCheck:
    def __init__(self, host='0.0.0.0', port=9999, docker_version='1.20.0', socket_version='2.3.1'):
        self.app = Flask(__name__)
        self.host = host
        self.port = port
        self.docker_version = docker_version
        self.socket_version = socket_version

    def setup_health_endpoint(self):
        @self.app.route('/health', methods=['GET'])
        def health_check():
            try:
                with Socket(AF_INET, SOCK_STREAM) as s:
                    s.bind((self.host, self.port))  # Verify socket can bind
                    s.listen(1)
                    s.close()
                    return jsonify(status='healthy', 
                                 docker_version=self.docker_version,
                                 socket_version=self.socket_version)
            except Exception as e:
                return jsonify(status='unreachable', error=str(e)), 500

    def run(self):
        self.setup_health_endpoint()
        self.app.run(host=self.host, port=self.port, debug=False)

# Initialize and start health check server
if __name__ == '__main__':
    health_check = SocketHealthCheck()
    health_check.run()

