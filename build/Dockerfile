ARG tag=2.2

FROM sannel/house.openfaasrunner:${tag} AS build
FROM build AS base
COPY --chown=house:house Function/* /tmp/
RUN mv /tmp/Function.dll /tmp/TmpFunction.dll \
    && cp /app/* /tmp/ \
    && rm -f /tmp/Function.dll \
    && mv /tmp/TmpFunction.dll /tmp/Function.dll \
    && rm -rvf /app/* \
    && mv -vr /tmp/* /app/