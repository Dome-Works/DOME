"""Add healthcheck for Sockets and "live" status bubble on Socket page (fix for issue #48)"""

import socket
from typing import Dict, Any
from datetime import datetime

class SocketHealthMonitor:
    """
    Monitors socket health and provides a 'live' status bubble representation.
    Used for documenting socket services in HomelabDocs.
    """
    
    def __init__(self, host: str = "localhost", port: int = 8080):
        self.host = host
        self.port = port
        self._is_healthy = False
    
    def check_socket_health(self) -> bool:
        """
        Perform a TCP connection test to determine if the socket is healthy.
        
        Returns:
            True if the socket is reachable, False otherwise.
        """
        try:
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.settimeout(5)
            result = sock.connect_ex((self.host, self.port))
            sock.close()
            self._is_healthy = (result == 0)
            return self._is_healthy
        except Exception:
            self._is_healthy = False
            return False
    
    def get_live_status_bubble(self) -> Dict[str, Any]:
        """
        Generate a dictionary representing the 'live' status bubble for documentation.
        
        Returns:
            Dictionary containing status information suitable for rendering in docs.
        """
        if not self.check_socket_health():
            return {
                "bubble_type": "degraded",
                "status": "not_healthy",
                "message": "Socket is unreachable",
                "host": self.host,
                "port": self.port,
                "checked_at": datetime.now().isoformat(),
                "healthy": False
            }
        
        return {
            "bubble_type": "live",
            "status": "healthy",
            "message": "Service is operational",
            "host": self.host,
            "port": self.port,
            "checked_at": datetime.now().isoformat(),
            "healthy": True
        }
    
    def update_status(self, healthy: bool) -> None:
        """
        Update internal state based on health check result.
        
        Args:
            healthy: Boolean indicating if the socket is healthy.
        """
        self._is_healthy = healthy

# Example usage for documentation integration
if __name__ == "__main__":
    monitor = SocketHealthMonitor(host="192.168.1.100", port=9000)
    status = monitor.get_live_status_bubble()
    print(f"[{status['bubble_type']}] {status['status']}: {status['message']}")
    print(f"Host: {status['host']}, Port: {status['port']}")
    print(f"Last checked: {status['checked_at']}")

