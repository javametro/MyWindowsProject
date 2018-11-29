
    var funclib = require('../../../funclib');
    var template = require('../../../template');
    var constant = require('../../../constant');
    module.exports = new template.Case({ 
        descrition:,
        category: constant.Category.Common,
        flags: [
            constant.Flag.Hardware.Battery,
            constant.Flag.OS.Win10
        ]
    })

    .check('text', (the_case, param) =>{
        var objShell = new ActiveXObject("WScript.Shell");
        var result = objShell.Run("test.bat 1.dll 12.0.24.0", 0, true);
        console.debug("check : " + param)
        the_case.assert.IsTrue(result)
    })
            