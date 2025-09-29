# Challenge

## Integrantes do Grupo

| Nome            |   RM   | Sala   |
|:----------------|:------:|:-------|
| Julia Monteiro  | 557023 | 2TDSPV |
| Victor Henrique | 556206 | 2TDSPH |
| Sofia Petruk    | 556585 | 2TDSPV |


## Instruções para Executação da API
### 1. Pré-requisitos
  - Certifique-se de ter o .NET SDK 8.0 ou superior instalado em sua máquina.

### 2. Clonar o Repositório
  - git clone https://github.com/sofiapetruk/sprint03-.net.git

### 3. Navegar até a pasta do projeto
  - cd .\Sprint02

### 4. Rodar a aplicação
  - dotnet run

### 5. Acesso a API
  - http://localhost:5192

## Exemplos de uso dos endpoints

### Usuários
| Método HTTP | Rota (URL)          | Descrição             | 
| :---        | :---                | :---                  | 
| POST        | [api/usuario]      | Cria um novo usuário  |
| GET         | [api/usuario]      | Busca várias usuários | 
| GET         | [api/usuario/{id}] | Busca um usuário      |
| PUT         | [api/usuario/{id}] | Atualizar um usuário  |
| DELETE      | [api/usuario/{id}] | Remover um usuário    |

### Motos
| Método HTTP | Rota (URL)          | Descrição             | 
| :---        | :---                | :---                  | 
| POST        | [api/moto]          | Cria uma nova moto    |
| GET         | [api/moto]          | Busca várias moto     | 
| GET         | [api/moto/{id}]     | Busca uma moto        |
| PUT         | [api/moto/{id}]     | Atualizar uma moto    |
| DELETE      | [api/moto/{id}]     | Remover uma moto      |

### Status da Moto
| Método HTTP | Rota (URL)              | Descrição                | 
| :---        | :---                    | :---                     | 
| POST        | [api/status-moto]       | Cria um novo status moto |
| GET         | [api/status-moto]       | Busca vários status moto | 
| GET         | [api/status-moto/{id}]  | Busca um status moto     |
| PUT         | [api/status-moto/{id}]  | Atualizar um status moto |
| DELETE      | [api/status-moto/{id}]  | Remover uma status moto  |

### Tipo de Moto
| Método HTTP | Rota (URL)            | Descrição              | 
| :---        | :---                  | :---                   | 
| POST        | [api/tipo-moto]       | Cria um novo tipo moto |
| GET         | [api/tipo-moto]       | Busca vários tipo moto | 
| GET         | [api/tipo-moto/{id}]  | Busca um tipo moto     |
| PUT         | [api/tipo-moto/{id}]  | Atualizar um tipo moto |
| DELETE      | [api/tipo-moto/{id}]  | Remover uma tipo moto  |



http://localhost:5192/index.html
http://localhost:5192/

http://localhost:5192/swagger/v1/swagger.json

dotnet run
