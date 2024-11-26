# UC-DE01: Notifica o Status do Pedido

## Identificação
**Nome do Caso de Uso:** Notifica o Envio  
**ID do Caso de Uso:** UC-DE01  
**Ator Primário:** Destinatário  
**Atores Secundários:** Sistema de Transporte  
**Objetivo:** Informar ao destinatário que o pedido foi enviado e fornecer detalhes sobre o envio.  

## Pré-condições
- O pedido deve estar registrado no sistema.  
- A transportadora deve ter confirmado o envio no sistema.  
- O destinatário deve ter um e-mail ou outro meio de notificação configurado.  

## Pós-condições
- O destinatário é notificado sobre o envio do pedido.  
- Informações sobre o status de envio são disponibilizadas ao destinatário.  

## Fluxo Principal de Ações
1. A transportadora atualiza o status do pedido para "Enviado" no sistema.  
2. O sistema identifica o destinatário associado ao pedido.  
3. O sistema gera uma notificação contendo:
   - Número do pedido  
   - Transportadora responsável  
   - Código de rastreamento  
   - Data estimada de entrega  
4. O sistema envia a notificação ao destinatário (via e-mail, SMS ou aplicativo).  
5. O destinatário visualiza a notificação e pode acessar o rastreamento do pedido.  

## Fluxos Alternativos

### Fluxo Alternativo A: Notificação Não Entregue
**Condição de ativação:** A notificação não é entregue ao destinatário (ex.: e-mail inválido).  
1. O sistema registra a falha no envio.  
2. O sistema tenta reenviar a notificação.  
3. Caso o problema persista, o sistema notifica o suporte para intervenção manual.  

## Requisitos Não Funcionais
- A notificação deve ser enviada em até 2 minutos após a atualização do status de envio.  
- O sistema deve suportar diferentes canais de notificação (ex.: e-mail, push notification).  


## Notas de Design e Observações
- A notificação deve incluir links diretos para o rastreamento da entrega.  
- O layout da mensagem deve ser responsivo, adequado para visualização em dispositivos móveis.  

# UC-DE02: Rastreia Entrega

## Identificação
**Nome do Caso de Uso:** Rastreia Entrega  
**ID do Caso de Uso:** UC-DE02  
**Ator Primário:** Destinatário  
**Atores Secundários:** Transportadora, Sistema de Notificação  
**Objetivo:** Permitir ao destinatário acompanhar o status da entrega de seu pedido, desde a saída do armazém até a entrega no endereço de destino.  


## Pré-condições
- O destinatário deve ter realizado um pedido no Marketplace.  
- O pedido já foi enviado pela transportadora.  
- O sistema de rastreamento deve estar em funcionamento.  


## Pós-condições
- O destinatário visualiza as atualizações do status da entrega.  
- O status da entrega é mantido atualizado até o recebimento final.  


## Fluxo Principal de Ações
1. O Destinatário acessa a seção de "Rastreamento de Entrega" no sistema.  
2. O sistema solicita o código de rastreamento associado ao pedido do destinatário.  
3. O Destinatário insere o código de rastreamento.  
4. O sistema consulta a Transportadora para obter a localização e o status do pedido.  
5. O sistema exibe o status atual do pedido, como: "Pedido enviado", "Em trânsito", "Saiu para entrega", etc.  
6. O Destinatário visualiza as atualizações de status e encerra a interação.  


## Fluxos Alternativos

### Fluxo Alternativo A: Rastreamento Automático
**Condição de ativação:** O código de rastreamento é fornecido automaticamente ao acessar a página de detalhes do pedido.  
1. O Sistema já possui o código de rastreamento do pedido.  
2. O sistema pula a etapa de inserção de código e consulta automaticamente a transportadora para o status de entrega.  
3. Fluxo retorna ao passo 5 no fluxo principal.  

### Fluxo Alternativo B: Código de Rastreamento Inválido
**Condição de ativação:** O destinatário insere um código de rastreamento inválido.  
1. O Sistema detecta que o código inserido não corresponde a nenhum pedido válido.  
2. O sistema notifica o Destinatário de que o código está incorreto e solicita a inserção de um novo código.  
3. Fluxo retorna ao passo 3 no fluxo principal.  


## Fluxos de Exceção

### Exceção 1: Falha na Comunicação com a Transportadora
**Condição de ativação:** O sistema falha ao se conectar com a transportadora para obter o status da entrega.  
1. O Sistema detecta uma falha na comunicação com a transportadora.  
2. O sistema notifica o Destinatário da falha e oferece a opção de tentar novamente mais tarde.  
3. O caso de uso termina ou retorna ao passo 4 do fluxo principal.  

### Exceção 2: Sistema de Rastreamento Fora do Ar
**Condição de ativação:** O sistema de rastreamento da transportadora está inoperante.  
1. O Sistema detecta que o serviço de rastreamento da transportadora está indisponível.  
2. O sistema notifica o Destinatário e recomenda tentar novamente mais tarde ou contatar o suporte.  
3. O caso de uso termina.  


## Requisitos Não Funcionais
- O sistema deve exibir o status de entrega em até 3 segundos após a consulta.  
- O sistema deve suportar múltiplos acessos simultâneos sem afetar o desempenho.  
- O sistema deve garantir 99,9% de disponibilidade para o serviço de rastreamento.  


## Notas de Design e Observações
- Integrar a API de rastreamento da transportadora para garantir atualizações em tempo real.  
- Garantir que o sistema armazene localmente o último status conhecido do pedido, para exibir em caso de indisponibilidade temporária do serviço da transportadora.  
- O sistema deve ser compatível com notificações push para que o destinatário receba atualizações automaticamente.  

---

# UC-DE03: Agenda dia de Recebimento

## Identificação
**Nome do Caso de Uso:** Agenda dia de Recebimento  
**ID do Caso de Uso:** UC-DE03  
**Ator Primário:** Destinatário  
**Atores Secundários:** Sistema de Transportadora, Vendedor  
**Objetivo:** Permitir ao destinatário escolher uma data preferencial para o recebimento da mercadoria.  

## Pré-condições
- O pedido deve estar aprovado e em processo de envio.  
- O sistema deve ter informações de disponibilidade da transportadora.  

## Pós-condições
- O dia de recebimento é registrado no sistema e comunicado à transportadora e ao vendedor.  
- O destinatário recebe confirmação da data agendada.  

## Fluxo Principal de Ações
1. O Destinatário acessa o sistema e seleciona a opção "Agendar Recebimento".  
2. O Sistema apresenta uma lista de datas disponíveis, conforme o status do envio e a disponibilidade da transportadora.  
3. O Destinatário escolhe uma data preferencial.  
4. O Sistema verifica a disponibilidade da data selecionada.  
5. O Sistema registra a data de recebimento no pedido e envia a confirmação ao destinatário, à transportadora e ao vendedor.  

## Fluxos Alternativos

### Fluxo Alternativo A: Data não disponível
**Condição de ativação:** A data escolhida pelo destinatário não está disponível.  
1. O Sistema informa que a data não está disponível.  
2. O Destinatário escolhe outra data da lista de opções.  
3. O fluxo retorna ao passo 4 do fluxo principal.  

### Fluxo Alternativo B: Pedido já saiu para entrega
**Condição de ativação:** O pedido já saiu para entrega antes do agendamento ser realizado.  
1. O Sistema notifica o destinatário que o pedido está em rota e não pode mais ser agendado.  
2. O caso de uso é encerrado.  

## Fluxos de Exceção

### Exceção 1: Falha de comunicação com a transportadora
**Condição de ativação:** O sistema falha ao obter a disponibilidade da transportadora.  
1. O Sistema notifica o destinatário da falha e sugere tentar novamente mais tarde.  
2. O caso de uso termina.  

### Exceção 2: Pedido cancelado ou devolvido
**Condição de ativação:** O pedido foi cancelado ou marcado como devolvido.  
1. O Sistema informa que o agendamento não pode ser realizado pois o pedido não está mais ativo.  
2. O caso de uso termina.  

## Requisitos Não Funcionais
- O sistema deve processar o agendamento em até 5 segundos após a escolha da data.  
- A disponibilidade do sistema deve ser de 99.9%, e a comunicação com a transportadora deve ocorrer em tempo real.  

## Notas de Design e Observações
- A interface deve ser intuitiva, permitindo ao destinatário visualizar facilmente as datas disponíveis.  
- O sistema deve ser capaz de lidar com alta demanda de agendamentos simultâneos.  
- Sugere-se implementar um buffer de cache para consultas de disponibilidade da transportadora, reduzindo a carga de processamento.  


---

# UC-DE04: Recebe NF e documentos

## Identificação
**Nome do Caso de Uso:** Recebe NF e documentos  
**ID do Caso de Uso:** UC-DE04  
**Ator Primário:** Destinatário  
**Atores Secundários:** Transportadora  
**Objetivo:** Permitir que o destinatário receba a nota fiscal (NF) e documentos associados ao pedido.  

## Pré-condições
- O pedido foi realizado com sucesso e enviado pela transportadora.  
- Os documentos estão disponíveis no sistema do marketplace.  

## Pós-condições
- O destinatário visualizou e baixou os documentos (NF e outros).  
- O sistema registra a confirmação de recebimento.  

## Fluxo Principal de Ações
1. O Destinatário acessa o sistema do Marketplace de Embalagens e navega até a seção "Meus Pedidos".  
2. O sistema apresenta a lista de pedidos recentes.  
3. O Destinatário seleciona o pedido correspondente à entrega aguardada.  
4. O sistema exibe os detalhes do pedido, incluindo a opção de visualizar e baixar a Nota Fiscal (NF) e outros documentos.  
5. O Destinatário clica na opção para visualizar ou baixar os documentos.  
6. O sistema gera um link de download para os arquivos e os disponibiliza ao destinatário.  
7. O Destinatário visualiza ou faz o download da NF e dos documentos.  
8. O sistema registra a ação de visualização ou download dos documentos.  

## Fluxos Alternativos

### Fluxo Alternativo A: Pedido não contém documentos disponíveis
**Condição de ativação:** O pedido foi enviado, mas os documentos ainda não foram disponibilizados pelo vendedor ou transportadora.  
1. O Destinatário seleciona o pedido na seção "Meus Pedidos".  
2. O sistema informa que os documentos ainda não estão disponíveis.  
3. O Destinatário pode optar por receber uma notificação quando os documentos estiverem prontos.  
4. O sistema registra a solicitação de notificação e retorna à tela principal.  

### Fluxo Alternativo B: Problema ao baixar documentos
**Condição de ativação:** Ocorre um erro ao tentar baixar os documentos, como falha na conexão ou arquivo corrompido.  
1. O Destinatário clica na opção para baixar a NF e os documentos.  
2. O sistema tenta gerar o link de download, mas falha.  
3. O sistema informa ao Destinatário que houve um erro e sugere tentar novamente mais tarde.  
4. O sistema registra a falha e retorna ao passo 6 do fluxo principal.  

## Fluxos de Exceção

### Exceção 1: Documentos não encontrados
**Condição de ativação:** O sistema não encontra a NF ou os documentos associados ao pedido.  
1. O sistema detecta que os documentos não estão disponíveis no momento.  
2. O sistema notifica o Destinatário sobre a ausência dos documentos e solicita contato com o suporte.  
3. O caso de uso retorna ao passo 2 do fluxo principal ou é encerrado.  

### Exceção 2: Falha de autenticação
**Condição de ativação:** O destinatário não está autenticado corretamente no sistema.  
1. O sistema detecta que o Destinatário não possui sessão válida ou está com credenciais incorretas.  
2. O sistema redireciona o Destinatário para a tela de login e solicita nova autenticação.  
3. Após o login, o caso de uso retorna ao passo 1 do fluxo principal.  

## Requisitos Não Funcionais
- O sistema deve permitir o download dos documentos em até 3 segundos.  
- O sistema deve estar disponível 99.9% do tempo, com tolerância máxima de downtime de 5 minutos.  
- O sistema deve garantir a segurança no armazenamento e transmissão dos documentos, utilizando criptografia SSL/TLS.  

## Notas de Design e Observações
- Garantir que a geração de links de download seja temporária e tenha expiração para evitar o acesso não autorizado.  
- Implementar um mecanismo de cache para otimizar a visualização de documentos frequentemente acessados.  
- Assegurar que o sistema suporte múltiplos formatos de documento (PDF, XML) e que esteja preparado para adaptar-se a legislações locais sobre a emissão de NF.  

---

# UC-DE05: Avalia Transportadora e Vendedor

## Identificação
**Nome do Caso de Uso:** Avalia Transportadora e Vendedor  
**ID do Caso de Uso:** UC-DE05  
**Ator Primário:** Destinatário  
**Atores Secundários:** Transportadora, Vendedor  
**Objetivo:** Permitir que o destinatário avalie a qualidade do serviço da transportadora e do vendedor após o recebimento do pedido.  

## Pré-condições
- O pedido deve ter sido entregue ao destinatário.  
- A avaliação deve estar disponível por um período específico após a entrega.  

## Pós-condições
- As avaliações do destinatário são registradas no sistema.  
- As notas ou comentários são associados à transportadora e ao vendedor.  

## Fluxo Principal de Ações
1. O destinatário acessa a seção de avaliação no sistema.  
2. O sistema exibe o pedido entregue e solicita que o destinatário avalie:
   - A transportadora (ex.: prazo de entrega, condição do pacote)  
   - O vendedor (ex.: qualidade do atendimento, conformidade do produto)  
3. O destinatário atribui notas (ex.: escala de 1 a 5 estrelas) e pode adicionar comentários opcionais.  
4. O destinatário confirma a avaliação.  
5. O sistema registra as notas e comentários associados ao pedido.  
6. O sistema exibe uma mensagem de agradecimento ao destinatário.  

## Fluxos Alternativos

### Fluxo Alternativo A: Destinatário Decide Não Avaliar
**Condição de ativação:** O destinatário opta por não enviar uma avaliação.  
1. O sistema registra que o destinatário não avaliou o pedido.  

## Fluxos de Exceção

### Exceção 1: Erro ao Registrar Avaliação
**Condição de ativação:** O sistema não consegue registrar a avaliação.  
1. O sistema informa ao destinatário que houve um erro.  
2. O sistema solicita que o destinatário tente novamente.  

## Requisitos Não Funcionais
- O sistema deve registrar as avaliações em menos de 5 segundos após a confirmação.  
- As avaliações devem ser armazenadas com segurança para evitar manipulação.  

## Notas de Design e Observações
- A interface de avaliação deve ser intuitiva e destacar campos opcionais (como comentários).  
- O sistema deve notificar o vendedor e a transportadora quando uma nova avaliação for registrada.  

---

# UC-DE06: Avalia Conformidade do Pedido

## Identificação
**Nome do Caso de Uso:** Avalia Conformidade do Pedido  
**ID do Caso de Uso:** UC-DE06  
**Ator Primário:** Destinatário  
**Atores Secundários:** Sistema de Rastreamento de Entrega, Sistema de Suporte (se acionado)  
**Objetivo:** O destinatário avalia a conformidade do pedido, verificando se os itens entregues estão de acordo com o que foi solicitado e em boas condições.  

## Pré-condições
- O pedido foi entregue ao destinatário.  
- O destinatário tem acesso ao sistema para visualizar os detalhes do pedido.  
- O sistema possui as informações sobre o pedido e os itens entregues.  

## Pós-condições
- O sistema registra a avaliação de conformidade realizada pelo destinatário.  
- Se houver algum problema identificado, o destinatário pode acionar o suporte para resolver a questão.  

## Fluxo Principal de Ações
1. O destinatário inicia o caso de uso ao receber o pedido e acessar o sistema para verificar a conformidade.  
2. O sistema apresenta os detalhes do pedido, incluindo itens solicitados, quantidades e descrições.  
3. O destinatário compara os itens entregues com as informações do sistema.  
4. O destinatário confirma a conformidade do pedido ou identifica discrepâncias (quantidade incorreta, produto danificado, etc.).  
5. O sistema registra a avaliação de conformidade realizada pelo destinatário.  
6. O sistema permite que o destinatário acione o suporte, se necessário.  

## Fluxos Alternativos

### Fluxo Alternativo A: Pedido Parcialmente Conformado
**Condição de ativação:** O destinatário identifica que alguns itens estão em conformidade, mas outros não.  
1. O destinatário marca quais itens estão conformes e quais estão em desacordo.  
2. O sistema registra essa avaliação parcial e permite que o destinatário acione o suporte para os itens com problemas.  
3. Fluxo retorna ao passo 6 do fluxo principal.  

### Fluxo Alternativo B: Avaliação Posterior
**Condição de ativação:** O destinatário decide não realizar a avaliação de conformidade imediatamente.  
1. O destinatário opta por adiar a avaliação e fecha o sistema.  
2. O sistema mantém o status de "pendente" para a avaliação do pedido.  
3. Fluxo retorna ao passo 1 do fluxo principal quando o destinatário reinicia o processo.  

## Fluxos de Exceção

### Exceção 1: Sistema Fora do Ar
**Condição de ativação:** O sistema não está acessível quando o destinatário tenta realizar a avaliação.  
1. O sistema exibe uma mensagem informando que o serviço está temporariamente indisponível.  
2. O destinatário é instruído a tentar novamente mais tarde.  
3. O caso de uso termina ou retorna ao passo 1 quando o sistema volta a estar online.  

### Exceção 2: Pedido Não Encontrado
**Condição de ativação:** O sistema não localiza o pedido do destinatário.  
1. O sistema informa ao destinatário que o pedido não foi encontrado.  
2. O destinatário é orientado a verificar se o pedido correto foi selecionado ou a contatar o suporte.  
3. O sistema registra o erro e o caso de uso termina ou retorna ao passo 1 após o problema ser resolvido.  

## Requisitos Não Funcionais
- O sistema deve responder dentro de 3 segundos ao apresentar os detalhes do pedido.  
- O sistema deve permitir que o destinatário complete a avaliação em menos de 5 minutos.  
- O sistema deve estar acessível 99,9% do tempo para permitir a avaliação de conformidade.  

## Notas de Design e Observações
- O sistema pode incluir a opção de anexar fotos ou comentários detalhados pelo destinatário sobre os itens não conformes.  
- O design da interface deve ser simples e intuitivo, facilitando a comparação entre os itens entregues e os detalhes do pedido.  
- Caso o destinatário acione o suporte, o sistema deve automaticamente associar a avaliação de conformidade à solicitação de atendimento.  

---

# UC-DE07: Aciona Suporte

## Identificação
**Nome do Caso de Uso:** Aciona Suporte  
**ID do Caso de Uso:** UC-DE07  
**Ator Primário:** Destinatário  
**Atores Secundários:** Suporte Técnico, Sistema de Atendimento ao Cliente  
**Objetivo:** Permitir que o Destinatário acione o suporte em caso de problemas com o pedido, entrega, ou documentação, e obter a resolução ou orientações necessárias.  

## Pré-condições
- O Destinatário deve estar autenticado no sistema.  
- O pedido deve estar registrado no sistema com status de "Em andamento" ou "Finalizado".  

## Pós-condições
- O Destinatário recebe uma confirmação de que o chamado foi aberto com sucesso.  
- O Suporte Técnico recebe o chamado com as informações necessárias para atender o pedido de suporte.  
- O sistema atualiza o status do chamado de suporte no perfil do Destinatário.  

## Fluxo Principal de Ações
1. O Destinatário acessa a seção de suporte no Marketplace de Embalagens.  
2. O Destinatário seleciona o pedido relacionado ao problema e clica em "Acionar Suporte".  
3. O sistema exibe um formulário de contato, solicitando detalhes sobre o problema.  
4. O Destinatário preenche o formulário com a descrição do problema e clica em "Enviar".  
5. O sistema valida as informações enviadas e exibe uma confirmação de abertura de chamado.  
6. O Suporte Técnico recebe o chamado e começa a análise do problema.  
7. O Destinatário recebe um número de protocolo para acompanhar o chamado.  

## Fluxos Alternativos

### Fluxo Alternativo A: Sistema fora do ar
**Condição de ativação:** O sistema de atendimento ao cliente está temporariamente fora do ar.  
1. O Destinatário tenta acionar o suporte, mas o sistema detecta a indisponibilidade.  
2. O sistema exibe uma mensagem informando que o suporte está temporariamente indisponível e sugere tentar novamente mais tarde.  
3. Fluxo retorna ao passo 1 quando o sistema volta ao ar.  

### Fluxo Alternativo B: Pedido não encontrado
**Condição de ativação:** O Destinatário tenta acionar suporte para um pedido que não está listado em sua conta.  
1. O sistema informa que o pedido não foi encontrado e sugere que o Destinatário verifique o número do pedido ou entre em contato diretamente por telefone.  
2. O fluxo termina.  

## Fluxos de Exceção

### Exceção 1: Falha no envio do formulário
**Condição de ativação:** A conexão do Destinatário cai durante o envio do formulário de suporte.  
1. O sistema detecta a falha e exibe uma mensagem de erro solicitando que o Destinatário tente novamente mais tarde.  
2. O caso de uso termina.  

### Exceção 2: Informações incompletas
**Condição de ativação:** O formulário de suporte é enviado com informações incompletas.  
1. O sistema detecta o problema e solicita ao Destinatário que complete os campos obrigatórios antes de reenviar o formulário.  
2. O fluxo retorna ao passo 4 do fluxo principal.  

## Requisitos Não Funcionais
- O sistema deve permitir a abertura de chamados 24 horas por dia, 7 dias por semana.  
- O sistema deve responder à submissão do formulário em até 2 segundos.  
- O sistema deve garantir que o suporte técnico consiga visualizar o chamado em até 5 minutos após a submissão.  
- O sistema deve estar disponível 99.9% do tempo.  

## Notas de Design e Observações
- O design da interface deve garantir que o formulário de suporte seja simples e acessível tanto em dispositivos móveis quanto em desktop.  
- Sugere-se o uso de mensagens claras e amigáveis durante a abertura de chamados, com foco em orientar o usuário em caso de erros.  
- O sistema deve ter um mecanismo para acompanhar o status do chamado diretamente pelo perfil do Destinatário, evitando a necessidade de chamadas externas para atualização.

---

# UC-B01: Busca por Categoria

## Identificação
**Nome do Caso de Uso:** Busca por Categoria  
**ID do Caso de Uso:** UC-B01  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário filtre embalagens disponíveis no marketplace com base em categorias predefinidas.  

## Pré-condições
- O usuário deve ter acesso ao sistema (não necessariamente autenticado).  
- Deve haver categorias previamente cadastradas no sistema.  

## Pós-condições
- O sistema retorna uma lista de embalagens pertencentes à categoria selecionada.  

## Fluxo Principal de Ações
1. O usuário acessa a funcionalidade de busca por categoria.  
2. O sistema exibe uma lista de categorias disponíveis.  
3. O usuário seleciona uma categoria.  
4. O sistema busca e exibe os produtos correspondentes à categoria escolhida.  

## Fluxos Alternativos

### Fluxo Alternativo A: Categoria Não Contém Produtos
**Condição de ativação:** A categoria selecionada não possui produtos disponíveis.  
1. O sistema exibe uma mensagem informando que não há produtos cadastrados na categoria selecionada.  

## Requisitos Não Funcionais
- O sistema deve retornar os resultados da busca em até 3 segundos.  

## Notas de Design e Observações
- As categorias devem ser exibidas de forma hierárquica, se aplicável (ex.: "Plásticos > PET").  
- A interface deve ser intuitiva e responsiva.  

---

# UC-B02: Busca por Material

## Identificação
**Nome do Caso de Uso:** Busca por Material  
**ID do Caso de Uso:** UC-B02  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário filtre embalagens com base no material utilizado (ex.: PET, papelão, vidro).  

## Pré-condições
- O usuário deve ter acesso ao sistema.  
- Os materiais devem estar cadastrados no sistema e associados aos produtos.  

## Pós-condições
- O sistema retorna uma lista de produtos filtrados pelo material selecionado.  

## Fluxo Principal de Ações
1. O usuário acessa a funcionalidade de busca por material.  
2. O sistema exibe uma lista de materiais disponíveis.  
3. O usuário seleciona um material.  
4. O sistema busca e exibe os produtos correspondentes ao material escolhido.  

## Fluxos Alternativos

### Fluxo Alternativo A: Material Não Encontrado
**Condição de ativação:** Não há produtos cadastrados para o material selecionado.  
1. O sistema exibe uma mensagem informando que não há produtos disponíveis para o material escolhido.  

## Requisitos Não Funcionais
- A busca deve retornar resultados em até 3 segundos.  

## Notas de Design e Observações
- A busca por material deve ser combinável com outros filtros (ex.: categoria, preço).  

---

# UC-B03: Busca por Quantidade Mínima

## Identificação
**Nome do Caso de Uso:** Busca por Quantidade Mínima  
**ID do Caso de Uso:** UC-B03  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário filtre embalagens com base na quantidade mínima disponível.  

## Pré-condições
- O usuário deve ter acesso ao sistema.  
- Os produtos devem ter suas quantidades mínimas registradas no sistema.  

## Pós-condições
- O sistema exibe os produtos que atendem ao critério de quantidade mínima.  

## Fluxo Principal de Ações
1. O usuário acessa a funcionalidade de busca por quantidade mínima.  
2. O sistema apresenta um filtro de busca com a opção de especificar a quantidade mínima desejada.  
3. O usuário insere o valor da quantidade mínima e confirma a busca.  
4. O sistema filtra os produtos no banco de dados com base na quantidade mínima especificada.  
5. O sistema exibe os produtos que atendem ao critério solicitado, incluindo detalhes como descrição, preço e quantidade disponível.  
6. O usuário pode refinar os resultados adicionando outros filtros (ex.: preço, material, categoria) ou selecionar um produto para mais informações.  

## Fluxos Alternativos

### Fluxo Alternativo A: Nenhum Produto Atende ao Critério
**Condição de ativação:** Não há produtos disponíveis com a quantidade mínima especificada.  
1. O sistema exibe uma mensagem informando que não há produtos disponíveis com a quantidade solicitada.  

## Requisitos Não Funcionais
- A busca deve ser realizada em até 5 segundos, mesmo com filtros combinados.  

## Notas de Design e Observações
- O filtro de quantidade mínima pode ser combinado com outros critérios (ex.: preço, categoria).  

---

# UC-B04: Busca por Tempo de Entrega

## Identificação
**Nome do Caso de Uso:** Busca por Tempo de Entrega  
**ID do Caso de Uso:** UC-B04  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário filtre produtos com base no prazo de entrega estimado.  

## Pré-condições
- O usuário deve ter acesso ao sistema.  
- Os produtos devem ter prazos de entrega associados.  

## Pós-condições
- O sistema retorna os produtos disponíveis que atendem ao prazo de entrega especificado.  

## Fluxo Principal de Ações
1. O usuário acessa a funcionalidade de busca por tempo de entrega.  
2. O sistema solicita ao usuário que insira ou selecione um intervalo de tempo (ex.: 1-3 dias, 4-7 dias).  
3. O sistema filtra os produtos com base no prazo definido e exibe os resultados.  

## Fluxos Alternativos

### Fluxo Alternativo A: Nenhum Produto Atende ao Critério de Tempo de Entrega
**Condição de ativação:** Não há produtos disponíveis para o prazo especificado.  
1. O sistema informa que não há produtos com o tempo de entrega desejado.  

## Requisitos Não Funcionais
- O sistema deve realizar a busca em até 3 segundos.  

## Notas de Design e Observações
- A interface deve exibir o tempo de entrega junto com os resultados para ajudar na comparação.  
- O filtro pode ser combinado com outros critérios, como preço ou material.  

---

# UC-B05: Busca por Preço

## Identificação
**Nome do Caso de Uso:** Busca por Preço  
**ID do Caso de Uso:** UC-B05  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário filtre produtos com base em um intervalo de preços.  

## Pré-condições
- O usuário deve ter acesso ao sistema.  
- Os produtos devem ter preços registrados no sistema.  

## Pós-condições
- O sistema retorna os produtos que atendem ao intervalo de preços definido.  

## Fluxo Principal de Ações
1. O usuário acessa a funcionalidade de busca por preço.  
2. O sistema solicita que o usuário insira ou selecione um intervalo de preços (ex.: R$10,00 - R$100,00).  
3. O sistema filtra e exibe os produtos que atendem ao intervalo especificado.  

## Fluxos Alternativos

### Fluxo Alternativo A: Nenhum Produto Atende ao Intervalo de Preços
**Condição de ativação:** Não há produtos no intervalo de preços definido pelo usuário.  
1. O sistema exibe uma mensagem informando que não há produtos disponíveis.  

## Requisitos Não Funcionais
- A busca deve ser concluída em até 3 segundos.  

## Notas de Design e Observações
- O sistema deve permitir filtros adicionais, como ordenação por menor ou maior preço.  

---

# UC-B06: Alerta de Disponibilidade

## Identificação
**Nome do Caso de Uso:** Alerta de Disponibilidade  
**ID do Caso de Uso:** UC-B06  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário configure alertas para ser notificado quando um produto indisponível voltar ao estoque.  

## Pré-condições
- O usuário deve estar autenticado no sistema.  
- Deve haver produtos atualmente indisponíveis no marketplace.  

## Pós-condições
- O sistema registra o alerta de disponibilidade e notifica o usuário quando o produto voltar ao estoque.  

## Fluxo Principal de Ações
1. O usuário acessa um produto indisponível.  
2. O sistema oferece a opção de configurar um alerta.  
3. O usuário confirma a configuração do alerta.  
4. O sistema registra o alerta e exibe uma mensagem de confirmação.  
5. Quando o produto volta ao estoque, o sistema notifica o usuário (ex.: e-mail ou push notification).  

## Fluxos Alternativos

### Fluxo Alternativo A: Produto Torna-se Disponível Antes da Configuração do Alerta
**Condição de ativação:** O produto volta ao estoque antes que o alerta seja configurado.  
1. O sistema informa que o produto está disponível e não permite configurar o alerta.  

## Requisitos Não Funcionais
- As notificações devem ser enviadas em tempo real assim que o produto estiver disponível.  

## Notas de Design e Observações
- O sistema deve garantir a privacidade do usuário ao enviar notificações.  
- Deve ser possível visualizar e gerenciar alertas configurados pelo usuário.  

---

# UC-B07: Busca por Dimensões

## Identificação
**Nome do Caso de Uso:** Busca por Dimensões  
**ID do Caso de Uso:** UC-B07  
**Ator Primário:** Buscador de Embalagens  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o usuário filtre embalagens com base em dimensões específicas (ex.: altura, largura, profundidade).  

## Pré-condições
- O usuário deve ter acesso ao sistema.  
- Os produtos devem ter dimensões registradas no sistema.  

## Pós-condições
- O sistema exibe os produtos que atendem às dimensões especificadas pelo usuário.  

## Fluxo Principal de Ações
1. O usuário acessa a funcionalidade de busca por dimensões.  
2. O sistema solicita que o usuário insira as dimensões desejadas.  
3. O sistema filtra os produtos e exibe os que atendem aos critérios definidos.  

## Fluxos Alternativos

### Fluxo Alternativo A: Nenhum Produto Atende às Dimensões Especificadas
**Condição de ativação:** Não há produtos que atendam às dimensões inseridas pelo usuário.  
1. O sistema informa que não há produtos disponíveis para as dimensões especificadas.  

## Requisitos Não Funcionais
- A busca deve retornar os resultados em até 3 segundos.  

## Notas de Design e Observações
- A interface deve permitir a seleção de intervalos ou medidas exatas para as dimensões.

---

# UC-C01: Adiciona Embalagens ao Carrinho

## Identificação
**Nome do Caso de Uso:** Adiciona Embalagens ao Carrinho  
**ID do Caso de Uso:** UC-C01  
**Ator Primário:** Comprador  
**Atores Secundários:** Sistema de Banco de Dados  
**Objetivo:** Permitir que o comprador selecione embalagens disponíveis e as adicione ao carrinho para compra futura.  

## Pré-condições
- O comprador deve estar autenticado no sistema.  
- O sistema deve ter embalagens disponíveis no estoque.  

## Pós-condições
- As embalagens selecionadas são adicionadas ao carrinho do comprador na quantidade desejada.  
- O sistema atualiza o carrinho de compras e mantém os itens até a finalização do pedido ou esvaziamento manual.  

## Fluxo Principal de Eventos
1. O ator primário (comprador) navega pelo catálogo de embalagens e seleciona um produto.  
2. O sistema exibe os detalhes do produto (quantidade disponível, preço, descrição).  
3. O ator primário insere a quantidade desejada e clica no botão “Adicionar ao Carrinho”.  
4. O sistema verifica a disponibilidade da quantidade solicitada.  
5. Se a quantidade estiver disponível, o sistema adiciona o item ao carrinho de compras.  
6. O sistema exibe uma confirmação de que o item foi adicionado ao carrinho com sucesso.  
7. O ator primário pode continuar comprando ou acessar o carrinho para revisar os itens.  

## Fluxos Alternativos

### Fluxo Alternativo A: Adicionar uma embalagem indisponível
**Condição de ativação:** O comprador solicita uma quantidade de embalagens maior do que o disponível em estoque.  
1. O sistema notifica o ator primário de que a quantidade solicitada excede o estoque disponível.  
2. O ator primário pode optar por ajustar a quantidade ou aguardar a reposição de estoque.  
3. Fluxo retorna ao passo 3 do fluxo principal.  

### Fluxo Alternativo B: Acessar carrinho de compras antes de adicionar item
**Condição de ativação:** O ator primário acessa o carrinho antes de adicionar um item.  
1. O sistema exibe o carrinho de compras com os itens previamente adicionados (se houver).  
2. O ator pode optar por continuar navegando no catálogo ou finalizar a compra.  

## Fluxos de Exceção

### Exceção 1: Falha no sistema de estoque
**Condição de ativação:** O sistema de estoque está temporariamente indisponível.  
1. O sistema notifica o ator primário sobre a falha na verificação de disponibilidade.  
2. O ator primário pode optar por tentar novamente mais tarde ou contatar o suporte.  
3. O caso de uso termina ou retorna ao passo 3 do fluxo principal.  

### Exceção 2: Sessão do comprador expirada
**Condição de ativação:** O comprador está inativo por muito tempo e a sessão expira.  
1. O sistema redireciona o ator primário para a página de login, notificando-o da expiração.  
2. O caso de uso retorna ao passo 1 após a reautenticação.  

## Requisitos Não Funcionais
- O sistema deve garantir que as atualizações de estoque sejam feitas em tempo real, evitando a adição de itens esgotados.  
- O sistema deve exibir a confirmação da adição ao carrinho em até 2 segundos.  
- O carrinho de compras deve ser persistente, permitindo que o comprador retorne mais tarde e encontre os itens ainda no carrinho.  

## Notas de Design e Observações
- O design da interface deve destacar o botão "Adicionar ao Carrinho" de forma visível, facilitando a interação do comprador.  
- Um contador de itens deve ser visível no ícone do carrinho, indicando o número de produtos já adicionados.  
- Considere implementar uma funcionalidade de "salvar para mais tarde" para itens não adquiridos imediatamente.  

---

# UC-C03: Realiza Pedido

## Identificação
**Nome do Caso de Uso:** Realiza Pedido  
**ID do Caso de Uso:** UC-C03  
**Ator Primário:** Comprador  
**Atores Secundários:** Sistema de Pagamento, Sistema de Entrega  
**Objetivo:** Permitir que o comprador finalize um pedido, escolhendo as opções de pagamento e envio disponíveis e confirmando a compra.  

## Pré-condições
- O comprador deve estar autenticado no sistema.  
- O carrinho de compras deve conter pelo menos um item.  
- O endereço de entrega deve estar cadastrado no sistema.  
- Métodos de pagamento válidos devem estar disponíveis.  

## Pós-condições
- O pedido é registrado no sistema.  
- O sistema gera uma confirmação de pedido.  
- O sistema notifica o ator secundário (Sistema de Pagamento) para processar o pagamento.  
- O sistema notifica o ator secundário (Sistema de Entrega) sobre o pedido, após confirmação do pagamento.  

## Fluxo Principal de Ações
1. O Comprador inicia o caso de uso clicando em "Ir para o Carrinho".  
2. O sistema exibe o Resumo do Carrinho (itens + quantidade).  
3. O Comprador clica em "Fechar Pedido".  
4. O sistema exibe as opções de endereço de entrega e solicita confirmação ou seleção de um novo endereço.  
5. O Comprador confirma o endereço desejado.  
6. O sistema atualiza o Resumo do Pedido (itens + quantidade + total + Frete).  
7. O sistema exibe as opções de métodos de pagamento e solicita que o comprador escolha uma opção.  
8. O Comprador seleciona o método de pagamento desejado.  
9. O sistema exibe o resumo final do pedido (itens + quantidade + total + Frete + método de pagamento selecionado).  
10. O Comprador revisa o pedido e confirma a finalização.  
11. O sistema processa o pedido, registra no banco de dados e notifica o Sistema de Pagamento.  
12. O Sistema de Pagamento confirma o pagamento para o sistema.  
13. O sistema gera uma confirmação de pedido e notifica o Sistema de Entrega.  
14. O sistema exibe uma página de confirmação com os detalhes do pedido e prazo de entrega para o Comprador.  

## Fluxos Alternativos

### Fluxo Alternativo A: Adicionar Novo Endereço
**Condição de ativação:** O comprador deseja utilizar um endereço de entrega diferente.  
1. O Comprador seleciona a opção de adicionar um novo endereço.  
2. O sistema solicita que o comprador insira os detalhes do novo endereço.  
3. O Comprador insere o endereço e confirma.  
4. O sistema registra o novo endereço e retorna ao passo 5 do fluxo principal.  

### Fluxo Alternativo B: Método de Pagamento Indisponível
**Condição de ativação:** O método de pagamento escolhido não está disponível.  
1. O sistema informa ao Comprador que o método de pagamento selecionado está indisponível.  
2. O Comprador escolhe um novo método de pagamento.  
3. O sistema retorna ao passo 7 do fluxo principal.  

### Fluxo Alternativo C: Alterar Itens do Carrinho
**Condição de ativação:** O Comprador altera a quantidade de um ou mais itens antes do passo 3.  
1. O Comprador digita ou clica no botão "+" ou no botão "-".  
2. O resumo do carrinho é recalculado.  

### Fluxo Alternativo D: Adicionar Cupom ao Pedido
**Condição de ativação:** O Comprador digita o código de cupom entre os passos 7 e 10.  
1. O Comprador digita o código.  
2. O sistema valida o código.  
3. O desconto associado ao código é aplicado ao total + Frete.  
4. O sistema recalcula o resumo final do pedido.  

## Fluxos de Exceção

### Exceção 1: Falha no Pagamento
**Condição de ativação:** O pagamento não é aprovado pelo Sistema de Pagamento.  
1. O Sistema de Pagamento notifica o sistema de uma falha na transação.  
2. O sistema informa ao Comprador que o pagamento não foi processado.  
3. O sistema retorna ao passo 7 do fluxo principal para que o comprador selecione outra forma de pagamento.  

## Requisitos Não Funcionais
- O sistema deve processar o pagamento e registrar o pedido em até 5 segundos.  
- O sistema deve ser capaz de lidar com 1000 pedidos simultâneos.  
- O sistema deve estar disponível 99.9% do tempo para a finalização de pedidos.  

## Notas de Design e Observações
- O sistema deve garantir a segurança nas transações de pagamento, utilizando criptografia para os dados sensíveis.  
- A integração com o Sistema de Pagamento e o Sistema de Entrega deve ser feita por meio de APIs RESTful.  
- O layout da página de confirmação deve seguir as diretrizes de usabilidade, apresentando de forma clara o número do pedido e o prazo estimado para entrega.  

---

# UC-C04: Cria Cadastro de Comprador

## Identificação
**Nome do Caso de Uso:** Cria Cadastro de Comprador  
**ID do Caso de Uso:** UC-C04  
**Ator Primário:** Comprador Autorizado  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que um usuário crie um cadastro como comprador no marketplace, inserindo informações pessoais e credenciais de acesso.  

## Pré-condições
- O usuário não deve ter um cadastro prévio no sistema.  
- O sistema deve estar disponível para acesso.  

## Pós-condições
- O comprador é registrado no sistema.  
- O sistema gera um identificador único para o comprador.  
- O comprador pode acessar funcionalidades que exigem autenticação.  

## Fluxo Principal de Ações
1. O usuário acessa a página de cadastro.  
2. O sistema exibe o formulário de cadastro.  
3. O usuário preenche o formulário com as seguintes informações:
   - Nome completo  
   - Endereço de e-mail  
   - Senha  
   - Dados adicionais, como endereço de entrega padrão e número de telefone.  
4. O sistema valida os dados fornecidos (e-mail único, formato correto de senha, etc.).  
5. O usuário confirma os dados clicando em "Cadastrar".  
6. O sistema registra as informações no banco de dados.  
7. O sistema exibe uma mensagem de confirmação e redireciona o comprador para a página inicial autenticada.  

## Fluxos Alternativos

### Fluxo Alternativo A: Dados Inválidos
**Condição de ativação:** O usuário insere informações inválidas ou incompletas.  
1. O sistema destaca os campos com erro e exibe mensagens explicativas.  
2. O sistema solicita que o usuário corrija os dados.  
3. O fluxo retorna ao passo 4 do fluxo principal.  

## Fluxos de Exceção

### Exceção 1: E-mail já cadastrado
**Condição de ativação:** O e-mail fornecido pelo usuário já está associado a uma conta.  
1. O sistema exibe uma mensagem informando que o e-mail já foi cadastrado.  
2. O sistema sugere ao usuário recuperar a senha.  

## Requisitos Não Funcionais
- O sistema deve validar os dados em menos de 2 segundos.  
- As senhas devem ser armazenadas usando criptografia robusta (ex.: hashing com sal).  

## Notas de Design e Observações
- O formulário deve ser responsivo e acessível em dispositivos móveis.  
- O sistema deve enviar um e-mail de boas-vindas ao comprador após o cadastro.  

---

# UC-C05: Acessa Histórico de Pedidos

## Identificação
**Nome do Caso de Uso:** Acessa histórico de pedidos  
**ID do Caso de Uso:** UC-C05  
**Ator Primário:** Comprador  
**Atores Secundários:** Nenhum  
**Objetivo:** Permitir que o comprador visualize o histórico de pedidos feitos no marketplace, com detalhes como status de entrega, data do pedido, e valores pagos.  

## Pré-condições
- O comprador deve estar autenticado no sistema.  
- O comprador deve ter realizado pelo menos um pedido anteriormente.  

## Pós-condições
- O sistema exibe ao comprador o histórico dos pedidos realizados.  
- O comprador poderá acessar os detalhes de um pedido específico a partir da lista exibida.  

## Fluxo Principal de Eventos
1. O ator primário (Comprador) inicia o caso de uso selecionando a opção de "Histórico de Pedidos" na interface.  
2. O sistema exibe ao comprador a lista de pedidos realizados, com informações resumidas de cada pedido (data, valor, status).  
3. O ator primário pode selecionar um pedido específico da lista.  
4. O sistema exibe os detalhes completos do pedido selecionado, incluindo itens comprados, valores, endereço de entrega, e status do pedido.  
5. O caso de uso é concluído quando o comprador decide fechar a visualização ou retornar para outra seção do marketplace.  

## Fluxos Alternativos

### Fluxo Alternativo A: Nenhum pedido realizado
**Condição de ativação:** O comprador não possui pedidos registrados no sistema.  
1. O sistema detecta que não há pedidos associados à conta do comprador.  
2. O sistema exibe uma mensagem informando que não há histórico de pedidos para exibir.  
3. O fluxo retorna ao início, permitindo que o comprador navegue para outras áreas do sistema.  

### Fluxo Alternativo B: Pedido arquivado
**Condição de ativação:** O comprador tenta acessar detalhes de um pedido antigo arquivado pelo sistema.  
1. O sistema identifica que o pedido selecionado foi arquivado.  
2. O sistema exibe uma mensagem ao comprador informando que detalhes completos do pedido não estão mais disponíveis.  
3. O fluxo retorna ao ponto em que o comprador pode selecionar outro pedido ou sair do histórico.  

## Fluxos de Exceção

### Exceção 1: Falha de conexão com o servidor
**Condição de ativação:** O sistema enfrenta problemas de conectividade ou indisponibilidade.  
1. O sistema detecta uma falha de conexão ao tentar acessar o histórico de pedidos.  
2. O sistema notifica o comprador sobre a falha e sugere tentar novamente mais tarde.  
3. O caso de uso termina, sem exibir o histórico de pedidos.  

## Requisitos Não Funcionais
- O sistema deve responder à solicitação de acesso ao histórico de pedidos em até 3 segundos.  
- O histórico de pedidos deve estar acessível ao comprador 99.9% do tempo.  
- A interface de exibição do histórico deve ser intuitiva e acessível em dispositivos móveis.  

## Notas de Design e Observações
- O sistema deve armazenar de forma segura as informações dos pedidos para que possam ser acessadas rapidamente, utilizando indexação eficiente para garantir tempo de resposta rápido.  
- Pode ser interessante implementar uma funcionalidade para exportar o histórico de pedidos para formatos como PDF ou CSV, proporcionando ao comprador uma forma de salvar os dados.  
- O sistema deve considerar a privacidade do comprador, ocultando informações sensíveis como métodos de pagamento em caso de consultas externas ao ambiente autenticado.  

---

# UC-C06: Recebe Sugestões de Anúncios

## Identificação
**Nome do Caso de Uso:** Recebe Sugestões de Anúncios  
**ID do Caso de Uso:** UC-C06  
**Ator Primário:** Comprador  
**Atores Secundários:** Sistema de Anúncios  
**Objetivo:** Fornecer ao comprador sugestões personalizadas de anúncios de produtos com base em seu histórico de navegação, compras ou preferências.  

## Pré-condições
- O comprador deve estar autenticado no sistema.  
- O comprador deve possuir algum histórico de interação no marketplace (ex.: visualizações, compras, pesquisas).  
- O Sistema de Anúncios deve estar configurado para oferecer sugestões.  

## Pós-condições
- O comprador recebe sugestões de anúncios personalizadas na interface do marketplace.  
- O sistema registra a interação do comprador com os anúncios para refinar futuras sugestões.  

## Fluxo Principal de Ações
1. O Comprador acessa a página inicial ou outra seção do marketplace.  
2. O sistema solicita ao Sistema de Anúncios sugestões baseadas no perfil do comprador.  
3. O Sistema de Anúncios retorna uma lista de anúncios relevantes.  
4. O sistema exibe os anúncios sugeridos em locais destacados da interface (ex.: banners, cards).  
5. O Comprador pode interagir com os anúncios (clicar, adicionar produtos ao carrinho, etc.).  
6. O sistema registra as interações do comprador com os anúncios para ajustar futuras sugestões.  

## Fluxos Alternativos

### Fluxo Alternativo A: Sem Histórico de Dados Suficiente
**Condição de ativação:** O comprador não possui histórico suficiente para gerar sugestões personalizadas.  
1. O Sistema de Anúncios retorna sugestões genéricas ou baseadas em tendências do marketplace.  
2. O sistema exibe essas sugestões na interface.  

## Fluxos de Exceção

### Exceção 1: Sistema de Anúncios Indisponível
**Condição de ativação:** O Sistema de Anúncios não responde à solicitação.  
1. O sistema exibe anúncios genéricos predefinidos no marketplace.  
2. O sistema registra a falha e notifica o administrador do sistema para manutenção.  

## Requisitos Não Funcionais
- As sugestões de anúncios devem ser geradas em menos de 3 segundos para garantir boa experiência do usuário.  
- O sistema deve utilizar algoritmos de recomendação com aprendizado de máquina para melhorar a relevância dos anúncios.  
- O sistema deve ser capaz de exibir anúncios em dispositivos de diferentes tamanhos de tela.  

## Notas de Design e Observações
- O layout das sugestões deve ser visualmente atraente, mas não intrusivo para o comprador.  
- Devem ser respeitadas as políticas de privacidade e proteção de dados ao gerar sugestões personalizadas.  
- Anúncios podem incluir promoções de vendedores que investem em campanhas de destaque.  



