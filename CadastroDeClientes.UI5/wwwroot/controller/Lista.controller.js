sap.ui.define([
	"invent/clientes/controller/BaseController",
	"sap/ui/core/routing/History",
	"sap/ui/model/json/JSONModel",
	"sap/ui/model/Filter",
	"sap/ui/model/FilterOperator"

], function (Controller, History, JSONModel, Filter, FilterOperator) {
	"use strict";

	return Controller.extend("invent.clientes.controller.Lista", {

		onInit: function () {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
			this.attachRouter("listaName", this.buscarNoServidor)
			let numeroDeClientes = {
				nome: "",
			};
			const oModel = new JSONModel(numeroDeClientes)
			this.getView().setModel(oModel, "busca");
		},
		
		onPress: function (oEvent) {
			var oItem = oEvent.getSource();
			var oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("detail", {
				id: window.encodeURIComponent(oItem.getBindingContext("cliente").getProperty("id"))
			});
		},
		
		onNavBack: function () {
				var oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("");
		},
		
		buscarNoServidor: async function (){
			this.handlePress("carregando");
			
			const dados = await fetch(`/api/Cliente`);
			const cliente = await dados.json();
			for (let i = 0; i < cliente.length; i++) {
				cliente[i].id = (cliente[i].id)
				cliente[i].cpf = this.colocarMascaraDeCpf(cliente[i].cpf)
			}

			for (let i = 0; i < cliente.length; i++) {
				cliente[i].id = (cliente[i].id)
				cliente[i].telefone = this.colocarMascaraDeTelefone(cliente[i].telefone)
			}
			const oModel = new JSONModel(cliente)
			this.getView().setModel(oModel, "cliente");
			
			this.handlePress("carregado");
		},

		buscarCliente : function (oEvent){
			// build filter array
			var aFilter = [];
			var sQuery = oEvent.getParameter("query");
			if (sQuery) {
				aFilter.push(new Filter("nome", FilterOperator.Contains, sQuery));
			}

			// filter binding
			var oTable = this.byId("listaclientes");
			var oBinding = oTable.getBinding("items");
			oBinding.filter(aFilter);
			
		},

		filtraCliente: function (oEvent) {
			// build filter array
			var aFilter = [];
			var sQuery = oEvent.getParameter("newValue");
			if (sQuery) {
				aFilter.push(new Filter("nome", FilterOperator.Contains, sQuery));
			}

			// filter binding
			var oTable = this.byId("listaclientes");
			var oBinding = oTable.getBinding("items");
			oBinding.filter(aFilter);

		},

		navegarParaCadastro : function (){
			var oRouter = this.getOwnerComponent().getRouter();	
				 oRouter.navTo("cadastroDeCliente");
		}
		
	});
});