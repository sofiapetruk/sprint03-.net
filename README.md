# Challenge

| Nome            |   RM   | Sala   |
|:----------------|:------:|:-------|
| Julia Monteiro  | 557023 | 2TDSPV |
| Victor Henrique | 556206 | 2TDSPH |
| Sofia Petruk    | 556585 | 2TDSPV |

## Definir do Desafio
Esta API RESTful foi desenvolvida para centralizar e automatizar o gerenciamento de ativos e registros de manutenção de motos, 
superando os desafios do rastreamento manual. Ela serve como o backbone de dados para sistemas de interface, como o SMARTMOTTU.

## Justificar o Domínio
  - Foi escolhida a entidade Usuário, pois precisamos identificar quem fez o cadastro em nosso site e verificar se ele cadastrou alguma moto.
  - Foi escolhida a entidade TipoMoto, pois precisamos saber quais motos estão disponíveis.
  - Foi escolhida a entidade StatusMoto, pois é necessário conhecer o estado de cada moto.
  - Foi escolhida a entidade Moto, pois precisamos vincular o proprietário da moto e relacionar as três entidades mencionadas anteriormente.
    
## Objetivo Principal da API
O objetivo é fornecer uma visão clara e completa da saúde e propriedade de cada moto, permitindo operações CRUD essenciais.

## Exemplos de uso dos endpoints

### Usuários
| Método HTTP | Rota (URL)            | Descrição             | 
| :---        | :---                  | :---                  | 
| POST        | [api/v1/Usuario]      | Cria um novo usuário  |
| GET         | [api/v1/Usuario]      | Busca várias usuários | 
| GET         | [api/v1/Usuario/{id}] | Busca um usuário      |
| PUT         | [api/v1/Usuario/{id}] | Atualizar um usuário  |
| DELETE      | [api/v1/Usuario/{id}] | Remover um usuário    |

### Motos
| Método HTTP | Rota (URL)             | Descrição             | 
| :---        | :---                   | :---                  | 
| POST        | [api/v1/Moto]          | Cria uma nova moto    |
| GET         | [api/v1/Moto]          | Busca várias moto     | 
| GET         | [api/v1/Moto/{id}]     | Busca uma moto        |
| PUT         | [api/v1/Moto/{id}]     | Atualizar uma moto    |
| DELETE      | [api/v1/Moto/{id}]     | Remover uma moto      |

### Status da Moto
| Método HTTP | Rota (URL)              | Descrição                   | 
| :---        | :---                    | :---                        | 
| POST        | [api/v1/Status-moto]       | Cria um novo status moto |
| GET         | [api/v1/Status-moto]       | Busca vários status moto | 
| GET         | [api/v1/Status-moto/{id}]  | Busca um status moto     |
| PUT         | [api/v1/Status-moto/{id}]  | Atualizar um status moto |
| DELETE      | [api/v1/Status-moto/{id}]  | Remover uma status moto  |

### Tipo de Moto
| Método HTTP | Rota (URL)               | Descrição              | 
| :---        | :---                     | :---                   | 
| POST        | [api/v1/Tipo-moto]       | Cria um novo tipo moto |
| GET         | [api/v1/Tipo-moto]       | Busca vários tipo moto | 
| GET         | [api/v1/Tipo-moto/{id}]  | Busca um tipo moto     |
| PUT         | [api/v1/Tipo-moto/{id}]  | Atualizar um tipo moto |
| DELETE      | [api/v1/Tipo-moto/{id}]  | Remover uma tipo moto  |

### Auth
| Método HTTP | Rota (URL)            | Descrição              | 
| :---        | :---                  | :---                   | 
| POST        | [api/v1/Auth/login]   | Gera um JWT (JSON Web Token) para um usuário fictício quando ele fizer login |

### Previsao
| Método HTTP | Rota (URL)            | Descrição              | 
| :---        | :---                  | :---                   | 
| POST        | [api/v1/Previsao]     | Recomendação Inteligente treinado para prever a moto mais adequada |

### Health
Endpoint para verificar se o seu bancos de dados está saúdavel
  - http://localhost:5192/health

## Documentação Interativa (Swagger)
  -  http://localhost:5192/swagger/index.html -- Para acessar o swagger e conseguir fazer os inputs dos métodos CRUD
  -  http://localhost:5192/swagger/v1/swagger.json -- Para acessar o swagger em json

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
  - http://localhost:5192/swagger/index.html
