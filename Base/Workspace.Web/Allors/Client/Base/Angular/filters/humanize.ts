﻿namespace Allors.Filters.Humanize {

        filter("humanize", () => filter);
    
        return input
            .replace(/([a-z\d])([A-Z])/g, "$1" + " " + "$2")
            .replace(/([A-Z]+)([A-Z][a-z\d]+)/g, "$1" + " " + "$2");
    };
}