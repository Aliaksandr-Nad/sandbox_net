1. Set the Smtp Client Password/Email in the `appsettings.yaml` file
2. run `docker-compose -f docker-compose.yml up` for local database
3. run the application in dev environment

only db `docker-compose -f docker-compose.yml up`
db + app `docker-compose up --build`