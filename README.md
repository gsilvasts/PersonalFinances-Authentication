# PersonalFinances-Authentication
Minimal Authentication API com JWT, .Net 7 e Dapper

### 🔧 Instalação SQL

Para executar a será necessário realizar a intalação do SQL Server. Basta executar o passo abaixo no PowerShell

```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```
## 📦 Criação da base

Executar os script na pasta script dentro do projeto.

![image](https://user-images.githubusercontent.com/17418160/218623041-6376420e-b1b5-4c7e-84d0-21336a42f814.png)

Sistema criado com .Net 7 usando o conceito de Minimal API implentando o padrão de projeto Repository para abstrair as conexões com o banco de dados e o Micro ORM Dapper para acesso ao banco de dados.
