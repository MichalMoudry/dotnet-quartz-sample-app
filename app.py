"""
Module with the sample app.
"""

import schedule
import time
from database import db_init

if __name__ == "__main__":
    engine = db_init.get_engine(True)
    db_init.create_tables(engine)

    schedule.every(5).seconds.do(print, "Hello!")
    with engine.connect() as conn:
        while True:
            schedule.run_pending()
            time.sleep(1)
