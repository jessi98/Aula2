# Gerenciador de Contatos - Windows Forms com C# e MySQL

Este projeto é um aplicativo desktop simples para gerenciamento de contatos, desenvolvido em C# utilizando Windows Forms para interface gráfica e MySQL para armazenamento de dados.

---

## Funcionalidades

- Adicionar novos contatos com nome, e-mail e telefone.
- Editar contatos existentes.
- Listar contatos cadastrados em uma lista detalhada.
- Buscar contatos por nome ou e-mail.
- Interface amigável com validações básicas.
- Integração com banco de dados MySQL para persistência dos dados.

---

## Tecnologias Utilizadas

- Linguagem C# (.NET Framework)
- Windows Forms (WinForms) para interface gráfica
- MySQL (via MySql.Data) para banco de dados
- MySqlConnector para conexão e execução de comandos SQL

---

## Como usar

### Pré-requisitos

- MySQL instalado e rodando localmente (localhost)
- Banco de dados criado chamado `aulas_uc3`
- Tabela `contato` criada com a seguinte estrutura:

```sql
CREATE TABLE contato (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100),
    email VARCHAR(100),
    telefone VARCHAR(20)
);
