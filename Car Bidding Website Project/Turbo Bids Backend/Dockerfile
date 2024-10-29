FROM gradle:8.7.0-jdk17
WORKDIR /opt/app
COPY ./build/libs/Turbo_Bids-1.0-SNAPSHOT.jar ./


ENTRYPOINT ["sh", "-c", "java ${JAVA_OPTS} -jar Turbo_Bids-1.0-SNAPSHOT.jar"]
