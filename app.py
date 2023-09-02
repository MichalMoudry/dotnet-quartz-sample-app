"""
Module with the sample app.
"""
import schedule
import time
from services import jobs
from database import engine, create_tables

if __name__ == "__main__":
    create_tables()

    with engine.connect() as conn:
        schedule.every(5).seconds.do(jobs.HelloJob().job)
        schedule.every(30).seconds.do(jobs.SqlReadJob().job)
        schedule.every(1).minute.do(jobs.SqlInsertJob().job)

        while True:
            schedule.run_pending()
            time.sleep(1)
