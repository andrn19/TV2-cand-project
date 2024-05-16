FROM --platform=linux/amd64 mysql:8.0.28
ENV MYSQL_ALLOW_EMPTY_PASSWORD yes
COPY /init.sql /docker-entrypoint-initdb.d/.
