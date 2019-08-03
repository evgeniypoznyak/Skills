FROM mongo
COPY *.json /
CMD mongoimport --host users --db Users --collection users --type json --file /users.json --jsonArray
