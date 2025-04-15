#!/bin/bash

sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2022.repo

sudo dnf install -y mssql-server
sudo /opt/mssql/bin/mssql-conf setup
sudo systemctl status mssql-server
sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
sudo firewall-cmd --reload
