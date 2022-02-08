#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/mssql/server:2019-latest as db

# Add the DACPAC and init data script to the image
WORKDIR "/data"
COPY Database/Roots.Data.Sql.dacpac .
COPY Database/RootsDataInit.sql .

# Configure external build arguments to allow configurability.
ARG DBNAME=Roots
ARG USER=sa
ARG PASSWORD=Password123!

# Configure environmental variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$PASSWORD
ENV MSSQL_PID=Express

# Expose standard port
EXPOSE 1433

# Install unzip, download sqlpackage, unzip and run sql server
# Wait for it to come up, and publish the DACPAC. Also run script to init data.
# Kill sql server and delete installed files
USER root
RUN apt-get update \
    && apt-get install unzip -y \
    && wget -q -O /var/opt/sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2185670 \
    && unzip -qq /var/opt/sqlpackage.zip -d /var/opt/sqlpackage \
    && rm /var/opt/sqlpackage.zip \
    && chmod +x /var/opt/sqlpackage/sqlpackage \
    && (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /var/opt/sqlpackage/sqlpackage /a:Publish /tsn:localhost /tdn:${DBNAME} /tu:${USER} /tp:${PASSWORD} \
         /sf:/data/Roots.Data.Sql.dacpac /p:IncludeCompositeObjects=true \
    && (/opt/mssql-tools/bin/sqlcmd -S localhost -d ${DBNAME} -U ${USER} -P ${PASSWORD} -i /data/RootsDataInit.sql) \
    && pkill sqlservr \
    && apt-get autoremove -y \
    && rm -rf /var/lib/apt/lists/* \
    && rm -rf /var/opt/sqlpackage \
    && rm -rf /data
