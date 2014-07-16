USE [trash]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

if exists(select 1 from sys.objects where name = 'data_nl_customer' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_customer](
	[cust_id] [int] NOT NULL,
	[name] [nvarchar](256) NOT NULL,
	[cust_type] [tinyint] NOT NULL,
	[company_type] [tinyint] NOT NULL,
	[reg_code] [varchar](50) NOT NULL,
	[vat_code] [varchar](20) NULL,
	[cust_ref_no] [varchar](20) NULL,
	[lang] [int] NOT NULL,
	[address_id] [int] NULL,
	[address_street] [nvarchar](256) NULL,
	[address_city] [nvarchar](50) NULL,
	[address_country] [nvarchar](50) NULL,
	[address_post_code] [nvarchar](50) NULL,
	[address_name] [nvarchar](256) NULL,
	[phone] [varchar](125) NULL,
	[fax] [varchar](50) NULL,
	[email] [nvarchar](256) NULL,
	[branch_id] [int] NOT NULL,
	[cust_group_id] [int] NOT NULL,
	[description] [nvarchar](256) NULL,
	[web_username] [varchar](19) NOT NULL,
	[web_psw] [nvarchar](60) NULL,
	[insurance_dt] [datetime] NULL,
	[insurance_amount] [int] NULL,
	[decision_dt] [datetime] NULL,
	[decision_no] [nvarchar](30) NULL,
	[decision_type] [int] NULL,
	[guarantee_type] [int] NULL,
	[guarantee_bank] [nvarchar](64) NULL,
	[guarantee_amount] [int] NULL,
	[guarantee_valid_from] [datetime] NULL,
	[guarantee_valid_to] [datetime] NULL,
	[guarantee_description] [nvarchar](256) NULL,
	[ins_decision_date] [datetime] NULL,
	[insf_evaluated_dt] [datetime] NULL,
	[insf_limit_amount] [int] NULL,
	[insf_risk_class] [int] NULL,
	[insf_monitoring] [int] NULL,
	[insf_last_mon_dt] [datetime] NULL,
	[insf_next_mon_dt] [datetime] NULL,
	[mng_name] [nvarchar](60) NULL,
	[mng_position] [nvarchar](80) NULL,
	[mng_phone] [nvarchar](41) NULL,
	[mng_email] [nvarchar](256) NULL,
	[mng_description] [nvarchar](256) NULL,
	[send_advertise] [bit] NOT NULL,
	[guarantee_amount_str] [varchar](100) NULL,
	[row_version] [timestamp] NOT NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_contract' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_contract](
	[cust_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[contract_type] [tinyint] NOT NULL,
	[contract_name] [nvarchar](256) NULL,
	[address1_id] [int] NULL,
	[address1_street] [nvarchar](256) NULL,
	[address1_city] [nvarchar](50) NULL,
	[address1_country] [nvarchar](50) NULL,
	[address1_post_code] [nvarchar](50) NULL,
	[address1_name] [nvarchar](256) NULL,
	[address2_id] [int] NULL,
	[address2_street] [nvarchar](256) NULL,
	[address2_city] [nvarchar](50) NULL,
	[address2_country] [nvarchar](50) NULL,
	[address2_post_code] [nvarchar](50) NULL,
	[address2_name] [nvarchar](256) NULL,
	[phone] [varchar](125) NULL,
	[fax] [varchar](50) NULL,
	[email] [nvarchar](256) NULL,
	[parent_id] [int] NULL,
	[ext_nav_code] [nvarchar](20) NULL,
	[contract_no] [nvarchar](64) NOT NULL,
	[fin_type] [int] NOT NULL,
	[valid_from] [datetime] NOT NULL,
	[valid_to] [datetime] NULL,
	[lang] [int] NOT NULL,
	[agent_id] [int] NULL,
	[state] [smallint] NOT NULL,
	[curr] [char](3) NOT NULL,
	[curr_rate_provider_id] [int] NULL,
	[inv_freq] [smallint] NOT NULL,
	[inv_series_id] [int] NULL,
	[inv_template_id] [int] NOT NULL,
	[send_inv] [bit] NOT NULL,
	[inv_show_dt] [bit] NOT NULL,
	[cred_dt_from] [datetime] NOT NULL,
	[max_debt_block] [bigint] NOT NULL,
	[max_debt_disc] [bigint] NOT NULL,
	[pay_term] [smallint] NOT NULL,
	[cred_term] [smallint] NOT NULL,
	[pay_delay] [smallint] NOT NULL,
	[fine] [smallmoney] NOT NULL,
	[disc_check_pay] [bit] NOT NULL,
	[disc_curr_period] [bit] NOT NULL,
	[disc_foreign] [bit] NOT NULL,
	[transfer_auto] [bit] NOT NULL,
	[mng_name] [nvarchar](60) NULL,
	[mng_position] [nvarchar](80) NULL,
	[mng_phone] [nvarchar](41) NULL,
	[mng_email] [nvarchar](256) NULL,
	[mng_description] [nvarchar](256) NULL,
	[description] [nvarchar](256) NULL,
	[block_reason] [nvarchar](256) NULL,
	[contract_group_id] [int] NOT NULL,
	[inv_send_email] [bit] NOT NULL,
	[data_temp_changed] [bit] NOT NULL,
	[inv_comment_id] [int] NULL,
	[agg_fuel_quantity] [bigint] NULL,
	[agg_penalty_type] [tinyint] NULL,
	[send_advertise] [bit] NOT NULL,
	[dd_agreement_no] [nvarchar](12) NULL,
	[credit_limit] [bigint] NOT NULL,
	[campaign_code] [varchar](50) NULL,
	[einvoice_address] [varchar](50) NULL,
	[sales_person] [nvarchar](256) NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_iban' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_iban](
	[contract_id] [int] NOT NULL,
	[type] [int] NOT NULL,
	[acc_number] [varchar](35) NOT NULL,
	[bank_code] [varchar](35) NOT NULL,
	[direct_debit] [bit] NULL,
	[direct_debit_no] [varchar](35) NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_transfer' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_transfer](
	[contract_id] [int] NOT NULL,
	[cust_id] [int] NULL,
	[file_name] [varchar](255) NULL,
	[transfer_no] [nvarchar](20) NULL,
	[transfer_line_no] [varchar](50) NULL,
	[payer_name] [nvarchar](200) NULL,
	[cust_iban] [varchar](35) NULL,
	[payer_no] [varchar](11) NULL,
	[primary_payer_no] [varchar](11) NULL,
	[primary_payer_name] [nvarchar](140) NULL,
	[recipient_iban] [varchar](35) NULL,
	[transfer_amount] [bigint] NOT NULL,
	[transfer_dt] [datetime] NOT NULL,
	[transfer_comment] [nvarchar](300) NULL,
	[netting] [bit] NOT NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_card' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_card](
	[contract_id] [int] NOT NULL,
	[cust_id] [int] NULL,
	[card_no] [varchar](19) NOT NULL,
	[state_block] [smallint] NOT NULL,
	[state_stolen] [smallint] NOT NULL,
	[state_lost] [smallint] NOT NULL,
	[check_days] [tinyint] NOT NULL,
	[month_limit1] [bigint] NOT NULL,
	[month_limit2] [bigint] NOT NULL,
	[month_limit3] [bigint] NOT NULL,
	[week_limit1] [bigint] NOT NULL,
	[week_limit2] [bigint] NOT NULL,
	[week_limit3] [bigint] NOT NULL,
	[day_limit1] [bigint] NOT NULL,
	[day_limit2] [bigint] NOT NULL,
	[day_limit3] [bigint] NOT NULL,
	[valid_from] [datetime] NOT NULL,
	[valid_to] [datetime] NOT NULL,
	[is_local] [tinyint] NOT NULL,
	[branch_id] [int] NULL,
	[description] [nvarchar](256) NULL,
	[block_description] [nvarchar](256) NULL,
	[prod_pin_block] [nvarchar](16) NULL,
	[prod_pin_encrypted] [nvarchar](16) NULL,
	[prod_track1] [nvarchar](100) NULL,
	[prod_track2] [nvarchar](100) NOT NULL,
	[prod_embos1] [nvarchar](32) NULL,
	[prod_embos2] [nvarchar](32) NULL,
	[prod_embos3] [nvarchar](32) NULL,
	[prod_embos4] [nvarchar](32) NULL,
	[rg_id] [int] NOT NULL,
	[address_id] [int] NULL,
	[address_street] [nvarchar](256) NULL,
	[address_city] [nvarchar](50) NULL,
	[address_country] [nvarchar](50) NULL,
	[address_post_code] [nvarchar](50) NULL,
	[address1_name] [nvarchar](256) NULL,
	[ask_odometer] [bit] NOT NULL,
	[ask_vrn] [bit] NOT NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_balance' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_balance](
	[contract_id] [int] NOT NULL,
	[cust_id] [int] NULL,
	[amount] [bigint] NOT NULL,
	[dt] [datetime] NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_disc' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_disc](
	[contract_id] [int] NOT NULL,
	[item_group] [tinyint] NOT NULL,
	[product_group] [varchar](8) NULL,
	[ca_id] [int] NULL,
	[ca_group_id] [int] NULL,
	[price_model] [int] NULL,
	[disc_type] [varchar](2) NULL,
	[disc0] [decimal](19, 6) NULL,
	[priority] [int] NOT NULL,
	[bound_type] [tinyint] NOT NULL,
	[bound1] [bigint] NULL,
	[disc1] [decimal](19, 6) NULL,
	[bound2] [bigint] NULL,
	[disc2] [decimal](19, 6) NULL,
	[bound3] [bigint] NULL,
	[disc3] [decimal](19, 6) NULL,
	[bound4] [bigint] NULL,
	[disc4] [decimal](19, 6) NULL,
	[bound5] [bigint] NULL,
	[disc5] [decimal](19, 6) NULL,
	[bound6] [bigint] NULL,
	[disc6] [decimal](19, 6) NULL,
	[bound7] [bigint] NULL,
	[disc7] [decimal](19, 6) NULL,
	[bound8] [bigint] NULL,
	[disc8] [decimal](19, 6) NULL,
	[bound9] [bigint] NULL,
	[disc9] [decimal](19, 6) NULL,
	[bound10] [bigint] NULL,
	[disc10] [decimal](19, 6) NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_ca' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_ca](
	[ca_id] [nvarchar](35) NOT NULL,
	[acq_inst] [varchar](11) NOT NULL,
	[brand_inst] [varchar](11) NOT NULL,
	[ca_name] [nvarchar](100) NOT NULL,
	[ca_address] [nvarchar](100) NOT NULL,
	[ca_city] [nvarchar](50) NULL,
	[ca_country] [char](3) NULL,
	[ca_phone] [nvarchar](20) NULL,
	[ca_email] [varchar](50) NULL,
	[state] [tinyint] NOT NULL,
	[ca_disc] [bit] NULL,
	[municipality_id] [char](4) NULL,
	[ca_type] [char](1) NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_term' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_term](
	[ca_id] [nvarchar](35) NOT NULL,
	[terminal_id] [nvarchar](16) NOT NULL,
	[frame_no] [nvarchar](30) NULL,
	[state] [tinyint] NOT NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_doc' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_doc](
	[contract_id] [int] NOT NULL,
	[cust_id] [int] NULL,
	[dt_from] [datetime] NOT NULL,
	[dt_to] [datetime] NOT NULL,
	[inv_dt] [datetime] NOT NULL,
	[pay_dt] [datetime] NULL,
	[amount] [money] NOT NULL,
	[amount_wo_vat] [money] NOT NULL,
	[paid_dt] [datetime] NULL,
	[paid_amount] [money] NOT NULL,
	[paid] [bit] NOT NULL,
	[inv_no] [int] NOT NULL,
	[inv_series] [nvarchar](25) NULL,
	[files] [nvarchar](4000) NULL,
	[doc_type] [int] NOT NULL,
	[ext_ref_no] [varchar](21) NULL,
	[inv_no_str] [nchar](100) NULL
) ON [PRIMARY]
GO

if exists(select 1 from sys.objects where name = 'data_nl_tran' and type = 'u')
  drop table data_nl_tran
go

CREATE TABLE [dbo].[data_nl_tran](
	[tran_id] [bigint] NOT NULL,
	[tran_det_id] [bigint] NOT NULL,
	[contract_id] [int] NULL,
	[inv_id] [int] NULL,
	[auth_code] [varchar](6) NOT NULL,
	[rcpt_no] [varchar](50) NOT NULL,
	[card_no] [varchar](19) NOT NULL,
	[acq_inst] [varchar](11) NOT NULL,
	[ca_id] [varchar](35) NOT NULL,
	[term_id] [varchar](16) NOT NULL,
	[loc_dt] [datetime] NOT NULL,
	[sys_dt] [datetime] NOT NULL,
	[curr_loc] [varchar](3) NOT NULL,
	[curr_rate] [numeric](18, 10) NOT NULL,
	[amount_loc_r] [int] NOT NULL,
	[amount_loc_vat_r] [int] NOT NULL,
	[amount_loc_excise_r] [int] NOT NULL,
	[amount_sys_r] [int] NOT NULL,
	[odometer] [varchar](50) NULL,
	[vrn] [varchar](50) NULL,
	[comment] [nvarchar](1024) NULL,
	[item_group] [tinyint] NOT NULL,
	[product_group] [varchar](50) NULL,
	[item_code] [varchar](50) NOT NULL,
	[item_name] [nvarchar](255) NOT NULL,
	[item_um] [varchar](3) NOT NULL,
	[item_quantity] [money] NOT NULL,
	[item_price] [money] NOT NULL,
	[amount_loc_rd] [int] NOT NULL,
	[amount_loc_vat_rd] [int] NOT NULL,
	[amount_sys_rd] [int] NOT NULL,
	[vat_rate] [smallmoney] NOT NULL,
	[amount_loc_excise_rd] [int] NOT NULL
) ON [PRIMARY]
GO


SET ANSI_PADDING OFF
GO

