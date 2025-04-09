---
name: FEATURE-REQUEST
about: Esse template é para issue de features
title: FEATURE-REQUEST
labels: ''
assignees: ''

---

name: "✨ Feature Request"
description: Sugira uma nova funcionalidade para o projeto
title: "✨ [Feature] - <título da funcionalidade>"
labels: ["enhancement"]

body:
  - type: input
    id: titulo
    attributes:
      label: "Título da funcionalidade"
      placeholder: Ex: Adicionar filtro por gênero nos jogos
    validations:
      required: true

  - type: textarea
    id: descricao
    attributes:
      label: "Descrição da funcionalidade"
      description: Explique o que a nova funcionalidade deve fazer e por quê
      placeholder: A funcionalidade permitirá que o usuário...
    validations:
      required: true

  - type: textarea
    id: beneficio
    attributes:
      label: "Benefícios"
      description: Quais benefícios essa funcionalidade traria para o sistema?
      placeholder: Melhor usabilidade, mais controle de administração etc.
    validations:
      required: false

  - type: checkboxes
    id: impactos
    attributes:
      label: Essa feature afeta:
      options:
        - label: A API
        - label: O banco de dados
        - label: A documentação
        - label: Os testes
