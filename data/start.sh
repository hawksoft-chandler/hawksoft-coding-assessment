#!/bin/bash

sleep 10

mongoimport --drop --host mongoDb --username root --password rootpassword --authenticationDatabase admin --db user-contacts --collection user-business-contacts --type json --jsonArray --file /mock-user-contacts.json
