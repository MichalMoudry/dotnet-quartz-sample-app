"""
Module with the sample app.
"""

import schedule
import time
from database import engine, create_tables
from services import jobs

if __name__ == "__main__":
    create_tables()

    schedule.every(5).seconds.do(jobs.HelloJob().job)
    with engine.connect() as conn:
        schedule.every(30).seconds.do(jobs.SqlReadJob().job)
        schedule.every(1).minute.do(jobs.SqlInsertJob().job)

        while True:
            schedule.run_pending()
            time.sleep(1)
