FROM mongo

COPY /mock-user-contacts.json /mock-user-contacts.json


ADD start.sh /start.sh
RUN chmod +x /start.sh

CMD ["/start.sh"]