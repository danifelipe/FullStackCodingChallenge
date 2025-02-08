#!/bin/bash
echo "Esperando a que SQL Server inicie..."
sleep 10
echo "Ejecutando script SQL..."
/opt/mssql-tools/bin/sqlcmd -S db -U sa -P YourPassword123 -d master -i /app/init-db.sql
