# Requisitos Funcionais

Seguimos o padrão IEEE para formalização dos requisitos funcionais.

## Requisitos Funcionais do UC-AE01:
- A aplicação deve permitir que o administrador acesse a seção de análise de produtos.
- A aplicação deve exibir uma lista de produtos com métricas de desempenho, incluindo vendas, devoluções e estoque restante.
- A aplicação deve permitir que o administrador selecione um produto para análise detalhada.
- A aplicação deve gerar gráficos e relatórios detalhados com dados históricos e tendências de desempenho dos produtos.
- A aplicação deve permitir que o administrador configure filtros personalizados, como período e categoria, para refinar os relatórios.
- A aplicação deve atualizar os relatórios com base nos filtros aplicados.
- A aplicação deve informar o administrador sobre inconsistências nos dados e sugerir ações corretivas, caso sejam detectadas.
- A aplicação deve registrar as análises realizadas para auditoria futura.
- A aplicação deve permitir a exportação de relatórios em formatos como PDF ou Excel.

## Requisitos Funcionais do UC-AE02:
- A aplicação deve permitir que o administrador acesse o painel de monitoramento do estoque.
- A aplicação deve exibir os níveis de estoque de todos os produtos em tempo real.
- A aplicação deve permitir que o administrador identifique produtos com estoque baixo ou em excesso.
- A aplicação deve permitir que o administrador configure alertas para itens com níveis críticos de estoque.
- A aplicação deve permitir que o administrador atualize registros de estoque manualmente, se necessário.
- A aplicação deve permitir que o administrador aplique filtros para visualizar o estoque de categorias específicas.
- A aplicação deve atualizar a exibição do painel com base nos filtros aplicados.

## Requisitos Funcionais do UC-AE03:
- A aplicação deve permitir que o administrador acesse o painel de monitoramento de vendas.
- A aplicação deve exibir o histórico de vendas e os dados de vendas recentes em tempo real.
- A aplicação deve permitir que o administrador filtre as vendas por período ou por produto.
- A aplicação deve atualizar os gráficos e relatórios com base nos filtros aplicados.
- A aplicação deve gerar relatórios detalhados de vendas, incluindo indicadores de performance, como gráficos de linha ou pizza.
- A aplicação deve permitir que o administrador utilize os relatórios para tomada de decisões estratégicas de reabastecimento.

## Requisitos Funcionais do UC-AE05:
- A aplicação deve monitorar os níveis de estoque de cada produto em tempo real.
- A aplicação deve gerar um alerta automaticamente quando um produto atingir o nível mínimo configurado.
- A aplicação deve enviar notificações de alerta para o administrador por e-mail e pelo aplicativo móvel.
- A aplicação deve permitir que o administrador visualize os detalhes do alerta, incluindo informações como produto, quantidade restante e recomendação de ação.
- A aplicação deve garantir que os níveis mínimos de estoque sejam configuráveis no sistema.

## Requisitos Funcionais do UC-DI01:
- A aplicação deve permitir que o distribuidor acesse a seção de devoluções ou trocas.
- A aplicação deve exibir uma lista de solicitações de devolução ou troca pendentes.
- A aplicação deve permitir que o distribuidor selecione uma solicitação para análise.
- A aplicação deve exibir os detalhes da solicitação e o histórico do pedido associado.
- A aplicação deve permitir que o distribuidor aprove ou rejeite uma solicitação de devolução ou troca.
- A aplicação deve registrar a decisão do distribuidor e atualizar o status da solicitação no sistema.
- A aplicação deve notificar o cliente sobre a decisão do distribuidor.
- A aplicação deve validar automaticamente se o prazo para devolução ou troca foi respeitado antes de permitir a aprovação.
- A aplicação deve solicitar informações adicionais ao cliente, caso a solicitação esteja incompleta.
- A aplicação deve permitir que o distribuidor retome a análise de uma solicitação após o cliente fornecer as informações necessárias.

## Requisitos Funcionais do UC-DI02:
- A aplicação deve permitir que o distribuidor acesse a seção de entregas futuras.
- A aplicação deve exibir uma lista de pedidos pendentes de entrega.
- A aplicação deve permitir que o distribuidor selecione os pedidos que deseja organizar para entrega.
- A aplicação deve sugerir uma prioridade para os pedidos com base na data de compra e na distância.
- A aplicação deve permitir que o distribuidor confirme ou ajuste manualmente as prioridades sugeridas.
- A aplicação deve agendar as entregas com base nas prioridades definidas.
- A aplicação deve notificar os clientes sobre os agendamentos realizados.
- A aplicação deve informar ao distribuidor sobre a logística necessária para cumprir os agendamentos.
- A aplicação deve permitir que o distribuidor ajuste manualmente os agendamentos, caso o sistema encontre problemas ao calcular as prioridades.

## Requisitos Funcionais do UC-DI03:
- A aplicação deve permitir que o cliente solicite uma solução logística personalizada.
- A aplicação deve permitir que o distribuidor analise as solicitações de logística personalizada feitas pelos clientes.
- A aplicação deve permitir que o distribuidor proponha uma solução logística personalizada.
- A aplicação deve registrar a proposta de solução personalizada no sistema.
- A aplicação deve notificar o cliente sobre os detalhes da solução proposta.
- A aplicação deve permitir que o cliente aceite ou rejeite a proposta de solução logística.
- A aplicação deve ajustar os agendamentos de entrega conforme a solução aprovada pelo cliente.
- A aplicação deve informar ao cliente caso a solução logística personalizada solicitada seja inviável.
- A aplicação deve sugerir opções logísticas padrão quando a personalização não for possível.

## Requisitos Funcionais do UC-DI04:
- A aplicação deve registrar os pedidos realizados pelos clientes no sistema.
- A aplicação deve verificar o estoque disponível para cada novo pedido registrado.
- A aplicação deve notificar o distribuidor sobre novos pedidos realizados.
- A aplicação deve permitir que o distribuidor visualize os detalhes dos novos pedidos.
- A aplicação deve adicionar os novos pedidos à fila de processamento automaticamente.
- A aplicação deve reenviar notificações ou alertar a equipe de suporte em caso de falha no envio ao distribuidor.
- A aplicação deve suportar notificações via e-mail e aplicativo móvel.

## Requisitos Funcionais do UC-V02:
- A aplicação deve permitir que o vendedor acesse a seção de gerenciamento de dados bancários.
- A aplicação deve exibir os dados bancários cadastrados atualmente (se houver).
- A aplicação deve permitir que o vendedor escolha entre cadastrar, editar ou remover uma conta bancária.
- A aplicação deve exibir um formulário correspondente para o cadastro, edição ou remoção de dados bancários.
- A aplicação deve permitir que o vendedor insira ou atualize os dados bancários e confirme as alterações.
- A aplicação deve validar os dados bancários inseridos utilizando o gateway de pagamento.
- A aplicação deve registrar as alterações realizadas nos dados bancários do vendedor.
- A aplicação deve exibir mensagens de sucesso ou erro após a validação das informações.
- A aplicação deve permitir que o vendedor corrija os dados em caso de erro e retente o cadastro ou edição.

## Requisitos Funcionais do UC-V03:
- A aplicação deve permitir que o vendedor acesse a seção de endereços de coleta no painel de controle.
- A aplicação deve exibir os endereços de coleta cadastrados atualmente (se houver).
- A aplicação deve permitir que o vendedor adicione, edite ou remova um endereço de coleta.
- A aplicação deve exibir um formulário correspondente à ação "Adicionar".
- A aplicação deve exibir um formulário correspondente à ação "Editar"
- A aplicação deve exibir um formulário correspondente à ação "Remover"
- A aplicação deve validar os dados do endereço fornecidos pelo vendedor.
- A aplicação deve registrar as alterações nos endereços de coleta após validação.
- A aplicação deve exibir mensagens de sucesso ou erro ao vendedor após a ação.
- A aplicação deve permitir que o vendedor corrija informações em caso de erro na validação e tente novamente.
- A aplicação deve suportar o cadastro de múltiplos endereços de coleta para o mesmo vendedor.

## Requisitos Funcionais do UC-V04:
- A aplicação deve permitir que o usuário acesse a página de cadastro de vendedor.
- A aplicação deve exibir um formulário de registro com os campos: Nome do vendedor ou empresa, CNPJ/CPF, Descrição do vendedor e Dados de contato.
- A aplicação deve permitir que o usuário preencha ou atualize os dados do perfil de vendedor.
- A aplicação deve validar as informações inseridas, incluindo CNPJ/CPF, com uma base oficial (ex.: Receita Federal).
- A aplicação deve registrar ou atualizar as informações do vendedor no banco de dados após a validação.
- A aplicação deve exibir mensagens de confirmação ao vendedor após o sucesso no cadastro ou atualização.
- A aplicação deve destacar campos incorretos e exibir mensagens explicativas em caso de dados inválidos.
- A aplicação deve permitir que o vendedor corrija os dados inválidos e tente novamente.
- A aplicação deve permitir que o vendedor associe um logotipo ou imagem ao seu perfil.

## Requisitos Funcionais do UC-V05:
- A aplicação deve permitir que o vendedor acesse a seção de relatórios no painel de controle.
- A aplicação deve solicitar que o vendedor selecione um intervalo de datas ou outros critérios de filtro para gerar o relatório.
- A aplicação deve permitir que o vendedor defina os critérios desejados e confirme a solicitação.
- A aplicação deve gerar e exibir relatórios detalhados contendo: Número de vendas realizadas, Valor total das vendas, Produtos mais vendidos e Taxas aplicadas.
- A aplicação deve permitir que o vendedor exporte o relatório gerado em formato PDF ou CSV.
- A aplicação deve exibir uma mensagem informando que não há dados disponíveis, caso o período selecionado não tenha registros de vendas.
- A aplicação deve permitir que o vendedor agende o envio de relatórios periódicos por e-mail.

## Requisitos Funcionais do UC-V06:
- A aplicação deve permitir que o vendedor acesse a opção "Oferecer Descontos Promocionais".
- A aplicação deve exibir a lista de produtos disponíveis no catálogo do vendedor.
- A aplicação deve permitir que o vendedor selecione um ou mais produtos para aplicar o desconto.
- A aplicação deve solicitar que o vendedor insira as condições e o percentual de desconto.
- A aplicação deve validar os dados inseridos.
- A aplicação deve processar as informações e aplicar o desconto nos produtos selecionados.
- A aplicação deve atualizar o catálogo com os preços promocionais e disponibilizar o desconto para os compradores.
- A aplicação deve exibir uma confirmação ao vendedor informando o sucesso da operação.
- A aplicação deve notificar o vendedor caso algum produto selecionado esteja fora de estoque, permitindo a alteração da seleção.
- A aplicação deve notificar o vendedor caso algum produto tenha sido removido do catálogo e deve removê-lo da seleção atual.

## Requisitos Funcionais do UC-V07:
- A aplicação deve permitir que o vendedor acesse a seção de mensagens ou dúvidas no painel de controle.
- A aplicação deve exibir uma lista com as dúvidas pendentes enviadas pelos compradores.
- A aplicação deve permitir que o vendedor selecione uma dúvida para responder.
- A aplicação deve exibir o histórico de comunicação relacionado à dúvida, se houver.
- A aplicação deve permitir que o vendedor insira uma resposta no campo de texto e confirme o envio.
- A aplicação deve registrar a resposta no sistema e vinculá-la ao histórico da dúvida.
- A aplicação deve notificar o comprador de que sua dúvida foi respondida.
- A aplicação deve informar ao vendedor quando não houver dúvidas pendentes e permitir a visualização de dúvidas respondidas anteriormente.
- A aplicação deve permitir busca e filtragem de dúvidas por critérios como produto, data ou status (pendente, respondida).
- A aplicação deve oferecer um editor de texto simples para que o vendedor formate as respostas (ex.: negrito, itálico).
- A aplicação deve permitir que o vendedor adicione links ou anexos às respostas, quando permitido pelas regras do marketplace.

## Requisitos Funcionais do UC-C01
- A aplicação deve permitir que o comprador navegue pelo catálogo de embalagens e visualize detalhes do produto, como quantidade disponível, preço e descrição.
- A aplicação deve permitir que o comprador insira a quantidade desejada de uma embalagem e clique em "Adicionar ao Carrinho".
- A aplicação deve verificar a disponibilidade da quantidade solicitada no estoque.
Se a quantidade solicitada estiver disponível, a aplicação deve adicionar o item ao carrinho de compras.
- A aplicação deve exibir uma confirmação para o comprador quando o item for adicionado ao carrinho com sucesso.
- A aplicação deve permitir que o comprador continue navegando pelo catálogo ou acesse o carrinho para revisar os itens.
- A aplicação deve notificar o comprador se a quantidade solicitada de uma embalagem exceder o estoque disponível e permitir ajustes na quantidade.
- A aplicação deve exibir o carrinho de compras com os itens previamente adicionados, caso o comprador opte por acessá-lo antes de adicionar um novo item.
- A aplicação deve notificar o comprador caso o sistema de estoque esteja temporariamente indisponível.
- A aplicação deve redirecionar o comprador para a página de login caso sua sessão expire, notificando-o da expiração.

## Requisitos Funcionais do UC-C03
- A aplicação deve ser capaz de cadastrar compradores únicos.
- A aplicação deve garantir que o carrinho contenha pelo menos um produto antes de realizar o pedido.
- A aplicação deve verificar a disponibilidade dos produtos no momento de realizar o pedido.
- A aplicação deve calcular e apresentar o resumo do carrinho, incluindo quantidade de itens e valor total.
- A aplicação deve permitir que o comprador selecione e confirme o endereço desejado.
- A aplicação deve exibir as opções de método de pagamento e permitir que o comprador escolha uma opção válida.
- A aplicação deve recalcular e atualizar o resumo final do pedido, considerando itens, quantidade, total e frete, além de aplicar qualquer método de pagamento selecionado.
- A aplicação deve permitir que o comprador adicione um novo endereço de entrega durante o processo de finalização do pedido.
- A aplicação deve informar ao comprador caso o método de pagamento selecionado esteja indisponível e permitir a escolha de uma nova opção.
- A aplicação deve permitir que o comprador altere a quantidade dos itens no carrinho antes de confirmar o pedido.
- A aplicação deve permitir que o comprador adicione um cupom de desconto ao pedido e recalcular o valor final.
- A aplicação deve notificar o comprador caso o pagamento falhe e permitir que selecione outro método de pagamento.
- A aplicação deve gerar uma confirmação de pedido com detalhes da compra, incluindo o prazo de entrega.


## Requisitos Funcionais do UC-C04:
- A aplicação deve permitir que o usuário acesse a página de cadastro.
- A aplicação deve exibir um formulário de cadastro com campos para nome completo, e-mail, senha, endereço de entrega padrão e número de telefone.
- A aplicação deve validar os dados fornecidos, verificando unicidade do e-mail, formato de senha e preenchimento de campos obrigatórios.
- A aplicação deve destacar campos inválidos e exibir mensagens explicativas em caso de erro.
- A aplicação deve permitir que o usuário confirme os dados clicando em "Cadastrar".
- A aplicação deve registrar os dados do usuário no banco de dados.
- A aplicação deve gerar um identificador único para o comprador.
- A aplicação deve exibir uma mensagem de confirmação após o cadastro.
- A aplicação deve redirecionar o comprador para a página inicial autenticada.
- A aplicação deve notificar o usuário caso o e-mail fornecido já esteja cadastrado.
- A aplicação deve sugerir ao usuário a recuperação de senha se o e-mail já estiver cadastrado.

## Requisitos Funcionais do UC-C05:
- A aplicação deve permitir que o comprador acesse a opção "Histórico de Pedidos" na interface.
- A aplicação deve exibir ao comprador uma lista de pedidos realizados com informações resumidas, como data, valor e status.
- A aplicação deve permitir que o comprador selecione um pedido específico da lista.
- A aplicação deve exibir os detalhes completos do pedido selecionado, incluindo itens comprados, valores, endereço de entrega e status do pedido.
- A aplicação deve informar ao comprador quando não houver pedidos registrados em sua conta.
- A aplicação deve exibir uma mensagem ao comprador quando os detalhes completos de um pedido arquivado não estiverem mais disponíveis.
- A aplicação deve permitir que o comprador retorne à lista de pedidos ou navegue para outra área do sistema.

## Requisitos Funcionais do UC-C06:
- A aplicação deve permitir que o comprador visualize sugestões de anúncios na página inicial ou em outras seções do marketplace.
- A aplicação deve solicitar ao Sistema de Anúncios sugestões personalizadas baseadas no perfil e histórico do comprador.
- A aplicação deve exibir os anúncios sugeridos em locais destacados da interface, como banners ou cards.
- A aplicação deve permitir que o comprador interaja com os anúncios, como clicar ou adicionar produtos ao carrinho.
- A aplicação deve registrar as interações do comprador com os anúncios para ajustar futuras sugestões.
- A aplicação deve exibir sugestões genéricas ou baseadas em tendências quando o comprador não possuir histórico suficiente.
- A aplicação deve exibir anúncios genéricos predefinidos caso o Sistema de Anúncios esteja indisponível.
- A aplicação deve registrar falhas no Sistema de Anúncios e notificar o administrador para manutenção.

## Requisitos Funcionais do UC-B01:
- A aplicação deve permitir que o usuário acesse a funcionalidade de busca por categoria.
- A aplicação deve exibir uma lista de categorias disponíveis.
- A aplicação deve permitir que o usuário selecione uma categoria.
- A aplicação deve buscar e exibir os produtos correspondentes à categoria selecionada.
- A aplicação deve informar o usuário caso a categoria selecionada não possua produtos disponíveis.

## Requisitos Funcionais do UC-B02:
- A aplicação deve permitir que o usuário acesse a funcionalidade de busca por material.
- A aplicação deve exibir uma lista de materiais disponíveis.
- A aplicação deve permitir que o usuário selecione um material.
- A aplicação deve buscar e exibir os produtos correspondentes ao material selecionado.
- A aplicação deve informar o usuário caso não haja produtos disponíveis para o material escolhido.

## Requisitos Funcionais do UC-B03:
- A aplicação deve permitir que o usuário acesse a funcionalidade de busca por quantidade mínima.
- A aplicação deve exibir um filtro de busca com a opção de especificar a quantidade mínima desejada.
- A aplicação deve permitir que o usuário insira o valor da quantidade mínima e confirme a busca.
- A aplicação deve filtrar os produtos no banco de dados com base na quantidade mínima especificada.
- A aplicação deve exibir os produtos que atendem ao critério solicitado, incluindo descrição, preço e quantidade disponível.
- A aplicação deve permitir que o usuário refine os resultados combinando outros filtros, como preço, material ou categoria.
- A aplicação deve informar ao usuário caso nenhum produto atenda ao critério de quantidade mínima especificada.

## Requisitos Funcionais do UC-B04:
- A aplicação deve permitir que o usuário acesse a funcionalidade de busca por tempo de entrega.
- A aplicação deve solicitar que o usuário insira ou selecione um intervalo de tempo para o prazo de entrega (ex.: 1-3 dias, 4-7 dias).
- A aplicação deve filtrar os produtos disponíveis com base no prazo de entrega definido.
- A aplicação deve exibir os produtos que atendem ao critério de tempo de entrega especificado.
- A aplicação deve informar o usuário caso nenhum produto atenda ao critério de tempo de entrega especificado.

## Requisitos Funcionais do UC-B05:
- A aplicação deve permitir que o usuário acesse a funcionalidade de busca por preço.
- A aplicação deve solicitar que o usuário insira ou selecione um intervalo de preços (ex.: R$10,00 - R$100,00).
- A aplicação deve filtrar os produtos com base no intervalo de preços definido.
- A aplicação deve exibir os produtos que atendem ao intervalo de preços especificado.
- A aplicação deve informar ao usuário caso nenhum produto esteja disponível no intervalo de preços definido.

## Requisitos Funcionais do UC-B06:
- A aplicação deve permitir que o usuário acesse produtos indisponíveis.
- A aplicação deve oferecer a opção de configurar um alerta para produtos indisponíveis.
- A aplicação deve permitir que o usuário confirme a configuração do alerta.
- A aplicação deve registrar o alerta de disponibilidade para o produto selecionado.
- A aplicação deve exibir uma mensagem de confirmação após o alerta ser configurado.
- A aplicação deve notificar o usuário quando o produto voltar ao estoque.
- A aplicação deve impedir a configuração de alertas para produtos que já estejam disponíveis.
- A aplicação deve permitir que o usuário visualize e gerencie os alertas configurados.

## Requisitos Funcionais do UC-B07:
- A aplicação deve permitir que o usuário acesse a funcionalidade de busca por dimensões.
- A aplicação deve solicitar que o usuário insira as dimensões desejadas (ex.: altura, largura, profundidade).
- A aplicação deve filtrar os produtos com base nas dimensões especificadas.
- A aplicação deve exibir os produtos que atendem aos critérios de dimensões definidos pelo usuário.
- A aplicação deve informar ao usuário caso nenhum produto atenda às dimensões especificadas.

## Requisitos Funcionais do UC-DE01:
- A aplicação deve permitir que a transportadora atualize o status do pedido para "Enviado".
- A aplicação deve identificar o destinatário associado ao pedido com status "Enviado".
- A aplicação deve gerar uma notificação contendo o número do pedido, transportadora responsável, código de rastreamento e data estimada de entrega.
- A aplicação deve enviar a notificação ao destinatário via e-mail, SMS ou aplicativo.
- A aplicação deve permitir que o destinatário visualize a notificação e acesse o rastreamento do pedido.
- A aplicação deve registrar falhas no envio de notificações, como e-mails inválidos.
- A aplicação deve tentar reenviar notificações em caso de falha no envio.
- A aplicação deve notificar o suporte para intervenção manual caso as notificações não sejam entregues após novas tentativas.

## Requisitos Funcionais do UC-DE02:
- A aplicação deve permitir que o destinatário acesse a seção de "Rastreamento de Entrega".
- A aplicação deve solicitar o código de rastreamento associado ao pedido do destinatário.
- A aplicação deve permitir que o destinatário insira o código de rastreamento.
- A aplicação deve consultar a transportadora para obter o status e a localização do pedido com base no código de rastreamento.
- A aplicação deve exibir o status atual do pedido, como "Pedido enviado", "Em trânsito", "Saiu para entrega", etc.
- A aplicação deve permitir que o destinatário visualize as atualizações de status.
- A aplicação deve realizar o rastreamento automaticamente caso o código esteja vinculado ao pedido e fornecido previamente.
- A aplicação deve notificar o destinatário se o código de rastreamento inserido for inválido, solicitando a correção.
- A aplicação deve notificar o destinatário sobre falhas na comunicação com a transportadora e permitir novas tentativas mais tarde.
- A aplicação deve notificar o destinatário caso o serviço de rastreamento da transportadora esteja indisponível e recomendar ações alternativas, como contatar o suporte.

## Requisitos Funcionais do UC-DE03:
- A aplicação deve permitir que o destinatário acesse a funcionalidade "Agendar Recebimento".
- A aplicação deve exibir uma lista de datas disponíveis para o recebimento, com base na disponibilidade da transportadora e no status do envio.
- A aplicação deve permitir que o destinatário selecione uma data preferencial para o recebimento.
- A aplicação deve verificar a disponibilidade da data escolhida pelo destinatário.
- A aplicação deve registrar a data de recebimento no pedido após confirmação da disponibilidade.
- A aplicação deve enviar uma confirmação da data agendada ao destinatário, à transportadora e ao vendedor.
- A aplicação deve notificar o destinatário se a data escolhida não estiver disponível e permitir a seleção de outra data.
- A aplicação deve informar ao destinatário que o agendamento não é possível caso o pedido já tenha saído para entrega.
- A aplicação deve notificar o destinatário sobre falhas na comunicação com a transportadora e sugerir tentar novamente mais tarde.
- A aplicação deve informar o destinatário caso o pedido esteja cancelado ou devolvido, impedindo o agendamento.

## Requisitos Funcionais do UC-DE04:
- A aplicação deve permitir que o destinatário acesse a seção "Meus Pedidos".
- A aplicação deve exibir uma lista de pedidos recentes ao destinatário.
- A aplicação deve permitir que o destinatário selecione um pedido específico na lista.
- A aplicação deve exibir os detalhes do pedido, incluindo opções para visualizar e baixar a Nota Fiscal (NF) e outros documentos associados.
- A aplicação deve gerar um link de download para os documentos e disponibilizá-lo ao destinatário.
- A aplicação deve registrar a ação de visualização ou download dos documentos pelo destinatário.
- A aplicação deve informar ao destinatário caso os documentos ainda não estejam disponíveis e permitir a solicitação de notificação quando eles forem disponibilizados.
- A aplicação deve notificar o destinatário caso ocorra um erro ao tentar baixar os documentos e sugerir tentar novamente mais tarde.
- A aplicação deve registrar falhas no download dos documentos.
- A aplicação deve notificar o destinatário caso os documentos associados ao pedido não sejam encontrados, orientando contato com o suporte.
- A aplicação deve redirecionar o destinatário para a tela de login caso não esteja autenticado corretamente.

## Requisitos Funcionais do UC-DE05:
- A aplicação deve permitir que o destinatário acesse a seção de avaliação.
- A aplicação deve exibir os pedidos entregues e solicitar ao destinatário que avalie a transportadora e o vendedor.
- A aplicação deve permitir que o destinatário atribua notas à transportadora e ao vendedor em uma escala predefinida (ex.: 1 a 5 estrelas).
- A aplicação deve permitir que o destinatário adicione comentários opcionais às avaliações.
- A aplicação deve registrar as notas e comentários associados ao pedido.
- A aplicação deve exibir uma mensagem de agradecimento após o destinatário confirmar a avaliação.
- A aplicação deve registrar que o destinatário optou por não avaliar o pedido, caso ele decida não enviar uma avaliação.
- A aplicação deve notificar o destinatário caso ocorra um erro ao registrar a avaliação e permitir que ele tente novamente.

## Requisitos Funcionais do UC-DE06:
- A aplicação deve permitir que o destinatário acesse a funcionalidade para avaliar a conformidade do pedido.
- A aplicação deve apresentar os detalhes do pedido, incluindo itens solicitados, quantidades e descrições.
- A aplicação deve permitir que o destinatário compare os itens entregues com os detalhes do pedido.
- A aplicação deve permitir que o destinatário confirme a conformidade total do pedido ou identifique discrepâncias.
- A aplicação deve registrar a avaliação de conformidade realizada pelo destinatário.
- A aplicação deve permitir que o destinatário marque quais itens estão conformes e quais não, em caso de avaliação parcial.
- A aplicação deve permitir que o destinatário acione o suporte para resolver problemas identificados.
- A aplicação deve permitir que o destinatário adie a avaliação de conformidade e retorne ao processo posteriormente.
- A aplicação deve notificar o destinatário caso o serviço esteja indisponível, sugerindo tentar novamente mais tarde.
- A aplicação deve informar o destinatário caso o pedido não seja encontrado, orientando a verificar os dados ou contatar o suporte.

## Requisitos Funcionais do UC-DE07:
- A aplicação deve permitir que o destinatário acesse a seção de suporte.
- A aplicação deve exibir uma lista de pedidos relacionados ao destinatário e permitir a seleção de um pedido para o suporte.
- A aplicação deve exibir um formulário de contato para o destinatário descrever o problema.
- A aplicação deve permitir que o destinatário preencha e envie o formulário com a descrição do problema.
- A aplicação deve validar as informações enviadas no formulário.
- A aplicação deve registrar o chamado de suporte e exibir uma confirmação de abertura para o destinatário.
- A aplicação deve gerar e fornecer um número de protocolo para acompanhamento do chamado.
- A aplicação deve notificar o suporte técnico com os detalhes do chamado.
- A aplicação deve informar o destinatário caso o serviço de atendimento ao cliente esteja indisponível, sugerindo tentar novamente mais tarde.
- A aplicação deve notificar o destinatário caso o pedido relacionado ao suporte não seja encontrado, sugerindo verificar os dados ou contatar o suporte por outro meio.
- A aplicação deve solicitar que o destinatário complete os campos obrigatórios caso o formulário seja enviado com informações incompletas.


### Total: 276 Requisitos funcionais em 33 casos de uso levantados