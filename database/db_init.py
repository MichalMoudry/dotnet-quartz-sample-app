"""
Module for initializing the database.
"""

from sqlalchemy import create_engine, Engine
from .model import Entity


def get_engine(enable_echo: bool) -> Engine:
    return create_engine(
        "sqlite+pysqlite:///:memory:",
        echo=enable_echo
    )


def create_tables(engine: Engine) -> None:
    Entity().metadata.create_all(engine)
