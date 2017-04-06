function complexRenderingField() { }

complexRenderingField.loadRenderingDefs = function(init) {
    return fieldBase.ajax("/live-list/LiveListComplexRenderingField/GetRenderingDefinitions", "", function (response) {
        var renderings = document.getElementById("ll-renderings");
        fieldBase.fillDropdown(renderings, response.RenderingDefs);

        if (init) {
            var valueObj = complexRenderingField.get();
            complexRenderingField.loadDatasources(valueObj.RenderingDefinition);
        }
    });
}

complexRenderingField.loadDatasources = function(renderingDefId) {
    var params = "&renderingDefId=" + renderingDefId;
    return fieldBase.ajax("/live-list/LiveListComplexRenderingField/GetDatasources", params, function (response) {
        var datasources = document.getElementById("ll-datasources");
        fieldBase.removeChildren(datasources);
        fieldBase.fillDropdown(datasources, response.Datasources);
    });
}

complexRenderingField.setValue = function() {
    var value = {
        RenderingDefinition: document.getElementById('ll-renderings').value,
        Datasource: document.getElementById('ll-datasources').value
    }
    fieldBase.getParentIframe().setAttribute('sc_value', JSON.stringify(value));
}

complexRenderingField.getValue = function() {
    var valueObj = complexRenderingField.get();

    document.getElementById('ll-renderings').value = valueObj.RenderingDefinition;
    document.getElementById('ll-datasources').value = valueObj.Datasource;
}

complexRenderingField.get = function() {
    var value = fieldBase.getParentIframe().getAttribute('sc_value');
    if (value === undefined || value.length == 0) {
        return;
    }

    return JSON.parse(value);
}

complexRenderingField.updateIframeDisplaySettings = function() {
    var parentIframe = fieldBase.getParentIframe();
    parentIframe.style = "height: 171px";
    parentIframe.scrolling = "no";
}