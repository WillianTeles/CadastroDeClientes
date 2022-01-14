sap.ui.define([
	"invent/clientes/controller/BaseController",
	"sap/ui/core/routing/History",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel"
], function (Controller, History, MessageBox, JSONModel) {
	"use strict";

	return Controller.extend("invent.clientes.controller.Atualizar", {
		onInit: function () {
			var oRouter = this.getOwnerComponent().getRouter();
			oRouter.getRoute("AtualizarName").attachPatternMatched(this._onObjectMatched, this);
		},

		_onObjectMatched: async function (oEvent) {
			this.Id = oEvent.getParameter("arguments").id;
			const dados = await fetch(`/api/Cliente/${this.Id}`);
			const cliente = await dados.json();
			const oModel = new JSONModel(cliente);
			this.getView().setModel(oModel, "cliente");
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

		setClienteModel: function (data) {
			var oModel = new JSONModel(data);
			this.getView().setModel(oModel, "cliente");
		},

		getClienteModel: function () {
			return this.getView().getModel("cliente").getData();
		},

		validaVazios: function (oEvent) {
			var nomeId = this.getView().byId('nome');
			var cPFId = this.getView().byId('cPF');
			var dataNascimentoId = this.getView().byId('dataNascimento');
			var telefoneId = this.getView().byId('telefone');

			var nomeValue = nomeId.getValue();
			var cPFValue = cPFId.getValue();
			var dataNascimentoValue = dataNascimentoId.getValue();
			var telefoneValue = telefoneId.getValue();

			if (nomeValue === "") {
				nomeId.setValueState("Error");
				MessageBox.error("Preencher Nome Completo");
				return false;
			}

			if (cPFValue === "") {
				cPFId.setValueState("Error");
				MessageBox.error("Preencher CPF");
				return false;
			}

			if (cPFValue.length < 10) {
				cPFId.setValueState("Error");
				MessageBox.error("CPF Inválido");
				return false;
			}

			if (dataNascimentoValue === "") {
				dataNascimentoId.setValueState("Error");
				MessageBox.error("Preencher Data de Nascimento");
				return false;
			}

			if (telefoneValue === "") {
				telefoneId.setValueState("Error");
				MessageBox.error("Preencher Telefone");
				return false;
			}

			if (telefoneValue.length < 10) {
				telefoneId.setValueState("Error");
				MessageBox.error("Telefone Inválido");
				return false;
			}

			nomeId.setValueState("None");
			cPFId.setValueState("None");
			dataNascimentoId.setValueState("None");
			telefoneId.setValueState("None");
		},

		salvarNoBancoDeDados: async function () {

			var passedValidation = this.validaVazios();

			if (passedValidation === false) {
				return false;
			}

			this.handlePress("carregando");

			let cliente = this.getClienteModel();

			const uri = await fetch('/api/Cliente', {
				method: 'PUT',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(cliente)

			});

			this.handlePress("carregado");

			if (uri.status == 200) {
				var oRouter = this.getOwnerComponent().getRouter();
				MessageBox.alert("Cliente alterado com sucesso!", {
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