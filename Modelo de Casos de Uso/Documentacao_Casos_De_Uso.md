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

