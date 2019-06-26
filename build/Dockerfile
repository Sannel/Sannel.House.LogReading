ARG tag=2.2

FROM sannel/house.openfaasrunner:${tag} AS base
COPY Function/Function.dll /app/
COPY Function/Sannel.*.dll /app/
USER root
RUN chown -R house:house /app
USER house