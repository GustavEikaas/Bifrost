Bifrost.namespace("Bifrost", {
    functionParser: {
        parse: function(func) {
            var result = [];

            var match = func.toString().match(/function\w*\s*\((.*?)\)/);
            if (match != null) {
                var arguments = match[1].split(/\s*,\s*/);
                arguments.forEach(function (item) {
                    if (item.trim().length > 0) {
                        result.push({
                            name: item
                        });
                    }
                });
            }

            return result;
        }
    }
});