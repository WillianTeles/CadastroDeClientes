sap.ui.define([
	"invent/clientes/controller/BaseController",
	"sap/ui/core/routing/History",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/m/MessageToast"

], function (Controller, History, MessageBox, JSONModel, MessageToast) {
	"use strict";

	return Controller.extend("invent.clientes.controller.Inserir", {

		onInit: function () {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
			this.setClienteModel(this.criandoModeloJsonCliente());
		},

		validaVazios: function (oEvent)
		{
			var nomeId = this.getView().byId('nome');
			var cPFId = this.getView().byId('cPF');
			var dataNascimentoId = this.getView().byId('dataNascimento');
			var telefoneId = this.getView().byId('telefone');

			var nomeValue = nomeId.getValue();
			var cPFValue = cPFId.getValue();
			var dataNascimentoValue = dataNascimentoId.getValue();
			var telefoneValue = telefoneId.getValue();

			if (nomeValue === "")
			{
				nomeId.setValueState("Error");
				MessageBox.error("Preencher Nome Completo");
				return false;
			}

			if (cPFValue === "")
			{
				cPFId.setValueState("Error");
				MessageBox.error("Preencher CPF");
				return false;
			}

			if (cPFValue.length < 10) {
				cPFId.setValueState("Error");
				MessageBox.error("CPF Inválido");
				return false;
			}

			if (dataNascimentoValue === "")
			{
				dataNascimentoId.setValueState("Error");
				MessageBox.error("Preencher Data de Nascimento");
				return false;
			}

			if (telefoneValue === "") {
				telefoneId.setValueState("Error");
				MessageBox.error("Preencher Telefone");
				return false;
			}

			if (telefoneValue.length < 10)
			{
				telefoneId.setValueState("Error");
				MessageBox.error("Telefone Inválido");
				return false;
            }

			nomeId.setValueState("None");
			cPFId.setValueState("None");
			dataNascimentoId.setValueState("None");
			telefoneId.setValueState("None");						
		},
		
		onNavBack: function () {
			var oHistory = History.getInstance();
			var sPreviousHash = oHistory.getPreviousHash();

			if (sPreviousHash !== undefined) {
				window.history.go(-1);
			} else {
				var oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("listaName", {}, true);
			}
		},

		criandoModeloJsonCliente: function(){
			let clientEmBranco =
			{	
			};
			return clientEmBranco;
		},

		setClienteModel: function (data) {
			var oModel = new JSONModel(data);
			this.getView().setModel(oModel, "cliente");
		},

		getClienteModel: function () {
			return this.getView().getModel("cliente").getData();
		},

		salvarNoBancoDeDados: async function (oEvent) {

			var passedValidation = this.validaVazios();

			if (passedValidation === false) {
				return false;
            }

			this.handlePress("carregando");

			let cliente = this.getClienteModel();

			const uri = await fetch('/api/Cliente', {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(cliente)
				
			});

			this.handlePress("carregado");

			if (uri.status == 200) {
				var oRouter = this.getOwnerComponent().getRouter();
				MessageBox.alert("Cliente salvo com sucesso!", {
					onClose: function () {
						oRouter.navTo("listaName", {}, true);
					}
				});
			}
			else {
				const content = await uri.json();
				var message = content.message
				MessageBox.alert(message);
            }			
		},
	});
});