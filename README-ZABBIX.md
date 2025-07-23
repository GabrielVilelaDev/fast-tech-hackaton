# Zabbix Monitoring Stack

Este projeto inclui uma stack completa do Zabbix para monitoramento, incluindo Zabbix Server, Zabbix Web Interface e Zabbix Agent 2.

## Componentes

- **MySQL 8.0**: Banco de dados para o Zabbix
- **Zabbix Server**: Servidor principal do Zabbix
- **Zabbix Web Interface**: Interface web para gerenciamento
- **Zabbix Agent 2**: Agente de monitoramento

## Como usar

### Iniciar o Zabbix

```powershell
docker-compose -f docker-compose.zabbix.yml up -d
```

### Parar o Zabbix

```powershell
docker-compose -f docker-compose.zabbix.yml down
```

### Verificar status dos containers

```powershell
docker-compose -f docker-compose.zabbix.yml ps
```

### Ver logs

```powershell
# Logs de todos os serviços
docker-compose -f docker-compose.zabbix.yml logs

# Logs de um serviço específico
docker-compose -f docker-compose.zabbix.yml logs zabbix-server
```

## Acesso

### Zabbix Web Interface
- **URL**: http://localhost:8081
- **Usuário padrão**: Admin
- **Senha padrão**: zabbix

### Portas expostas

- **8081**: Zabbix Web Interface
- **10051**: Zabbix Server (para agentes)
- **10050**: Zabbix Agent 2
- **3306**: MySQL Database (apenas interna)

## Configuração inicial

1. Aguarde todos os containers iniciarem (pode levar alguns minutos na primeira execução)
2. Acesse http://localhost:8081
3. Faça login com as credenciais padrão (Admin/zabbix)
4. **IMPORTANTE**: Altere a senha padrão em "Administration" > "Users"

### Configuração do Host Local

Após o login, configure o host local:

1. Vá em **Configuration** > **Hosts**
2. Você verá "Zabbix server" na lista
3. Clique no host "Zabbix server"
4. Na aba **Interfaces**, verifique se há uma interface do tipo "Agent" configurada
5. Se não houver, clique em **Add** e configure:
   - **Type**: Agent
   - **IP address**: zabbix-agent2
   - **Port**: 10050
6. Na aba **Templates**, adicione templates como:
   - Linux by Zabbix agent
   - Generic by Zabbix agent
7. Clique em **Update**

## Monitoramento

O Zabbix Agent 2 já está configurado para monitorar o próprio servidor Zabbix. Você pode:

- Adicionar novos hosts através da interface web
- Configurar templates de monitoramento
- Criar dashboards personalizados
- Configurar alertas e notificações

## Volumes persistentes

Os dados são armazenados em volumes Docker nomeados:
- `zabbix_mysql_data`: Dados do MySQL
- `zabbix_server_data`: Dados do servidor Zabbix
- `zabbix_server_scripts`: Scripts de alerta
- `zabbix_server_modules`: Módulos personalizados

## Rede

Todos os serviços estão isolados na rede `zabbix-network` para garantir comunicação segura entre os componentes.

## Status dos Serviços

### ✅ Correções aplicadas:
- Configuração otimizada do Zabbix Server com pollers específicos
- Agent 2 configurado corretamente para comunicação com o server
- Remoção de configurações privilegiadas que causavam problemas no Windows
- Configuração de rede isolada e funcional

### 🔧 Troubleshooting

#### Container não inicia
```powershell
docker-compose -f docker-compose.zabbix.yml logs [nome-do-servico]
```

#### Agent não conecta ao Server
1. Verifique se todos os containers estão rodando:
   ```powershell
   docker-compose -f docker-compose.zabbix.yml ps
   ```
2. Verificar logs do agent:
   ```powershell
   docker-compose -f docker-compose.zabbix.yml logs zabbix-agent2
   ```

#### Reset completo (CUIDADO: apaga todos os dados)
```powershell
docker-compose -f docker-compose.zabbix.yml down -v
docker-compose -f docker-compose.zabbix.yml up -d
```

#### Verificar conectividade entre containers
```powershell
docker-compose -f docker-compose.zabbix.yml exec zabbix-server ping mysql-server
```
