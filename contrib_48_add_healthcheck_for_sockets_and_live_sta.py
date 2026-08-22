"""Add healthcheck for Sockets and "live" status bubble on Socket page (fix for issue #48)"""

import socket
import time
from dataclasses import dataclass
from typing import Tuple, Optional


@dataclass
class SocketStatus:
    """Represents the health status of a socket connection."""

    is_alive: bool
    latency_ms: Optional[float] = None
    error: Optional[str] = None
    timestamp: float = None

    def __post_init__(self):
        if self.timestamp is None:
            self.timestamp = time.time()

    @property
    def bubble_color(self) -> str:
        """Return a color class for the status bubble."""
        if self.is_alive:
            return "bg-green-100 text-green-800"
        return "bg-red-100 text-red-800"

    @property
    def bubble_text(self) -> str:
        """Return text for the status bubble."""
        if self.is_alive:
            return "Live"
        return "Dead"


class SocketHealthChecker:
    """Checks the health of a socket connection and provides UI-ready status."""

    def __init__(self, host: str, port: int, timeout: float = 5.0):
        self.host = host
        self.port = port
        self.timeout = timeout

    def check_health(self) -> SocketStatus:
        """Check if the socket is alive and responsive, measuring latency.

        Returns:
            SocketStatus: Object containing alive status, latency, and any error message.
        """
        start_time = time.time()
        try:
            with socket.create_connection((self.host, self.port), self.timeout) as sock:
                latency = (time.time() - start_time) * 1000
                return SocketStatus(
                    is_alive=True,
                    latency_ms=round(latency, 2),
                    error=None,
                )
        except (socket.timeout, ConnectionRefusedError, OSError) as e:
            return SocketStatus(
                is_alive=False,
                latency_ms=None,
                error=str(e),
            )

    def get_status_bubble(self) -> Tuple[str, str]:
        """Get bubble color and text for direct UI display.

        Returns:
            Tuple[str, str]: (color_class, bubble_text) ready for use in a frontend.
        """
        status = self.check_health()
        return status.bubble_color, status.bubble_text

