FROM ubuntu

ARG DEBIAN_FORNTEND=nonteractive
RUN apt update
RUN apt install apache2 -y

EXPOSE 80

CMD apachectl -D FOREGROUND