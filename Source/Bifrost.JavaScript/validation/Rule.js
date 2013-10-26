Bifrost.namespace("Bifrost.validation", {
    Rule: Bifrost.Type.extend(function (ruleName, options) {
        var self = this;
        this.name = ruleName;
        this.handler = Bifrost.validation.ruleHandlers[ruleName];

        options = options || {};

        this.message = options.message || "";
        this.options = {};
        Bifrost.extend(this.options, options);

        this.validate = function (value) {
            return self.handler.validate(value, self.options);
        };
    })
});