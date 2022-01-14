sap.ui.define([
	"sap/ui/core/mvc/Controller",
	'sap/ui/Device'
], function (Controller, Device) {
	"use strict";

	return Controller.extend("invent.clientes.controller.App", {
		onInit: function () {
			Device.media.attachHandler(function (oDevice) {
				if (oDevice.name === "Phone") {
				}
				else if (this._sLastDevice !== oDevice.name) {
					this._sLastDevice = oDevice.name;

					if ((oDevice.name === "Tablet" && this._bExpanded) ||
						(oDevice.name === "Desktop" && !this._bExpanded)) {

						this.onSideNavButtonPress();
					}
				}
			}.bind(this));
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		},
		navegarParaLista : function (){
			var oRouter = this.getOwnerComponent().getRouter();
				 oRouter.navTo("listaName");
		},
		onItemSelect: function (oEvent) {
			let oItem = oEvent.getParameter('item');
			let sKey = oItem.getKey();
			var oRouter = this.getOwnerComponent().getRouter();
			if (Device.system.phone) {
				this.onSideNavButtonPress();
			}
			oRouter.navTo(sKey);
		},
	});
});