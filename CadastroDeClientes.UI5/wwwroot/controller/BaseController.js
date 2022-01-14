sap.ui.define([
    "sap/ui/core/mvc/Controller"
], function (Controller) {
    "use strict";

    return Controller.extend("invent.clientes.controller.Base", {
            
       getRouter: function (){
         return this.getOwnerComponent().getRouter();  
       },
        
        attachRouter(routerName, func){
           const router =   this.getRouter();
           
           if (!!routerName){
               router.getRoute(routerName).attachPatternMatched(func, this)
           }else {
               router.attachRouter(func, this);
           }           
        },

        colocarMascaraDeCpf: function (cpf) {
            return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4")
        },

        colocarMascaraDeTelefone: function (telefone) {
            return telefone.replace(/(\d{2})(\d{5})(\d{4})/, "($1)$2-$3")
        },

        navegarParaCadastro : function (){
			var oRouter = this.getOwnerComponent().getRouter();
				 oRouter.navTo("cadastroDeCliente");
		},
        
        handlePress: function (estado) {
            var oDialog = this.byId("BusyDialog");
            
            if(estado === "carregando"){
                oDialog.open();
            }
            if (estado === "carregado"){
                oDialog.close();
            }
        }
    });
});