
function fill_elem(id) {
    const root = document.getElementById(id);
    const lastNode = root.lastElementChild;
    const clone = lastNode.cloneNode(true);
    clone.querySelectorAll('input').forEach(input => {
        input.value = '';  // เคลียร์ค่าใน input field
    });
    document.getElementById(id).appendChild(clone);
    if (root.hasChildNodes()) updateCuntter(root.className)
}
function remove_elem(elemTarget, idRoot, nameParent) {
    const root = document.getElementById(idRoot);
    //console.log(root);
    const closest = elemTarget.closest(`div[name="${nameParent}"]`);
    if (root.hasChildNodes() && closest) {
        if (root.children.length > 1) {
            closest.remove();
        }
        updateCuntter(root.className)
    }
}
function updateCuntter(className) {
    if (className == 'fill_text') updateCounter_fillText()
    //if (className == 'fill_select') updateCounter_fillSelect()
}
function updateCounter_fillText() {
    // เลือกทุก .input-group ที่อยู่ภายใน .fill_text
    const inputGroups = document.querySelectorAll('.fill_text .input-group');

    // วนลูปและอัปเดตตัวเลขลำดับในแต่ละ .input-group
    inputGroups.forEach((group, index) => {
        group.style.counterReset = `rowNumber ${index}`;
    });
}
function addRow(id) {
    // Get the table body element
    const tableBody = document.getElementById(id);

    // Create a new row element
    const newRow = document.createElement('tr');

    // Define the HTML for the new row (เปลี่ยนเป็น textarea และไม่มี placeholder)
    newRow.innerHTML = `
                                <td class="text-center align-middle bg-owner"><textarea class="form-control bg-transparent"></textarea></td>
                                <td class="text-center align-middle bg-owner"><textarea class="form-control bg-transparent"></textarea></td>
                                <td class="text-center align-middle bg-owner"><textarea class="form-control bg-transparent"></textarea></td>
                                <td class="text-center align-middle bg-owner"><textarea class="form-control bg-transparent"></textarea></td>
                                <td class="text-center align-middle bg-owner"><textarea class="form-control bg-transparent"></textarea></td>
                                <td class="text-center align-middle">
                                    <button onclick="deleteRow(this)" class="btn btn-link text-danger"><i class="fas fa-trash-alt"></i></button>
                                </td>
                            `;

    // Add the new row to the table body
    tableBody.appendChild(newRow);
}
function deleteRow(button) {
    // Find the row to delete by using button's parent row
    const row = button.closest('tr');
    row.remove();
}
function getPlayload_By_tBodyid(id) {
    const tableBody = document.getElementById(id);
    const cardid = tableBody.closest('.card').dataset.cardid;
    const rows = tableBody.querySelectorAll('tr');
    const payload = [];

    rows.forEach(row => {
        const cells = row.querySelectorAll('textarea');
        if (cells.length > 0) {
            if (cells[0].value) {
                const rowData = {
                    cardid: cardid,
                    dtl_id: id,
                    type: 'project',
                    assessor: 'owner',
                    topic: cells[0].value,
                    objective: cells[1].value,
                    method: cells[2].value,
                    result: cells[3].value,
                    stakeholders: cells[4].value
                };
                payload.push(rowData);
            }
        }
    });

    console.log(JSON.stringify(payload)); // Shows payload in console

    return (payload);
}

function getPayloadByTBodyIdForRadios(id) {
    const tableBody = document.getElementById(id);
    const cardid = tableBody.closest('.card').dataset.cardid;
    const rows = tableBody.querySelectorAll('tr');
    const payload = [];

    // Loop through each row to get the selected radio value
    rows.forEach(row => {
        const radioName = row.querySelector('input[type="radio"]')?.name; // Get the name of radio group
        if (radioName) {
            const selectedRadio = row.querySelector(`input[name="${radioName}"]:checked`);
            const radioValue = selectedRadio ? selectedRadio.value : null;
            const project_ans_id = row.dataset.project_ans_id;
            if (radioValue) { // Only include rows with selected radio
                const rowData = {
                    cardid: cardid,
                    dtl_id: id,
                    type: 'project',
                    assessor: 'approver',
                    //----------
                    project_ans_id: project_ans_id,
                    value: radioValue, // Value from the selected radio
                };
                payload.push(rowData);
            }
        }
    });

    return payload;
}
function getJSON_input() {

    const cards = document.querySelectorAll('.card');
    const payload = [];


    cards.forEach(card => {
        const cardid = card.dataset.cardid;
        if (cardid) {
            const cardBodys = card.querySelector('.card-body');
            const inputElems = cardBodys.querySelectorAll('[data-category="input"]');


            inputElems.forEach(elem => {
                //console.log(elem);

                const type = elem.dataset.type;
                const kpiformdtl_id = elem.dataset.rowdtlid;
                switch (type) {
                    case "kpi_holding":
                        const kpi_holdings = elem.querySelectorAll(`select[name="kpi_holding"], span[name="kpi_holding"]`);

                        kpi_holdings.forEach(item => {
                            let selectedValue;
                            let kpicode, assessor, txtAreaVal;

                            if (item.tagName === "SELECT") {
                                // หากเป็น <select>
                                selectedValue = item.value;
                                kpicode = item.dataset.kpicode;
                                assessor = item.dataset.assessor;
                            } else if (item.tagName === "SPAN") {
                                // หากเป็น <span>
                                selectedValue = item.textContent.trim(); // อ่านค่าจากเนื้อหาใน <span>
                                kpicode = item.dataset.kpicode;
                                assessor = item.dataset.assessor;
                            }

                            // ค้นหา textarea ที่เกี่ยวข้อง
                            txtAreaVal = elem.querySelector(`textarea[data-kpicode="${kpicode}"][data-assessor="${assessor}"]`)?.value;

                            // ตรวจสอบเงื่อนไขและเพิ่มลงใน payload
                            if (kpicode && assessor && selectedValue) { // เช็คว่ามี kpicode
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    kpicode: kpicode,
                                    value: selectedValue,
                                    reason: txtAreaVal,
                                };

                                payload.push(rowData);
                            }
                        });
                        break;

                    case "kpi_competency":
                        const kpi_competencys = elem.querySelectorAll(`select[name="kpi_competency"], span[name="kpi_competency"]`);

                        kpi_competencys.forEach(item => {
                            let selectedValue;
                            let kpicode, assessor, txtAreaVal;

                            if (item.tagName === "SELECT") {
                                // หากเป็น <select>
                                selectedValue = item.value;
                                kpicode = item.dataset.kpicode;
                                assessor = item.dataset.assessor;
                            } else if (item.tagName === "SPAN") {
                                // หากเป็น <span>
                                selectedValue = item.textContent.trim(); // อ่านค่าจากเนื้อหาใน <span>
                                kpicode = item.dataset.kpicode;
                                assessor = item.dataset.assessor;
                            }

                            // ค้นหา textarea ที่เกี่ยวข้อง
                            txtAreaVal = elem.querySelector(`textarea[data-kpicode="${kpicode}"][data-assessor="${assessor}"]`)?.value;

                            // ตรวจสอบเงื่อนไขและเพิ่มลงใน payload
                            if (kpicode && assessor && selectedValue) { // เช็คว่ามี kpicode
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    kpicode: kpicode,
                                    value: selectedValue,
                                    reason: txtAreaVal,
                                };

                                payload.push(rowData);
                            }
                        });
                        break;
                    case "kpi_competency_reason":
                        const kpi_competency_reasons = elem.querySelectorAll(`select[name="kpi_competency_reason"], span[name="kpi_competency_reason"]`);

                        kpi_competency_reasons.forEach(item => {
                            let selectedValue;
                            let kpicode, assessor, txtAreaVal;

                            if (item.tagName === "SELECT") {
                                // หากเป็น <select>
                                selectedValue = item.value;
                                kpicode = item.dataset.kpicode;
                                assessor = item.dataset.assessor;
                            } else if (item.tagName === "SPAN") {
                                // หากเป็น <span>
                                selectedValue = item.textContent.trim(); // อ่านค่าจากเนื้อหาใน <span>
                                kpicode = item.dataset.kpicode;
                                assessor = item.dataset.assessor;
                            }

                            // ค้นหา textarea ที่เกี่ยวข้อง
                            txtAreaVal = elem.querySelector(`textarea[data-kpicode="${kpicode}"][data-assessor="${assessor}"]`)?.value;

                            // ตรวจสอบเงื่อนไขและเพิ่มลงใน payload
                            if (kpicode && assessor && selectedValue) { // เช็คว่ามี kpicode
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    kpicode: kpicode,
                                    value: selectedValue,
                                    reason: txtAreaVal,
                                };

                                payload.push(rowData);
                            }
                        });
                        break;
                    case "project":
                        const objDataTxtArea = getPlayload_By_tBodyid(kpiformdtl_id)
                        const objResApproval = getPayloadByTBodyIdForRadios(kpiformdtl_id)
                        objDataTxtArea.forEach((row) => {
                            payload.push(row);
                        })
                        objResApproval.forEach((row) => {
                            payload.push(row);
                        })

                        break;
                    case "cbo":
                        const cbos = elem.querySelectorAll(`select[name="cbo"]`);

                        cbos.forEach(cbo => {
                            let cboValue = cbo.value;
                            const assessor = cbo.dataset.assessor;

                            if (cboValue && !cbo.disabled) {
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    cboid: cboValue,
                                };
                                payload.push(rowData);
                            }
                        });
                        break;
                    case "cboGroup":
                        const cboGroups = elem.querySelectorAll(`select.detailCbo`);

                        cboGroups.forEach(cbo => {
                            let cboValue = cbo.value;
                            const assessor = cbo.dataset.assessor;

                            if (cboValue && !cbo.disabled) {
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    cboid: cboValue,
                                };
                                payload.push(rowData);
                            }
                        });
                        break;
                    case "textarea":
                        const textareas = elem.querySelectorAll(`textarea`);

                        textareas.forEach(txtarea => {
                            let txtVal = txtarea.value
                            const assessor = txtarea.dataset.assessor;
                            if (txtVal && !txtarea.readonly) {
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    value: txtVal,
                                };
                                payload.push(rowData);
                            }
                        });
                        break;
                    case "career":
                        const careers = elem.querySelectorAll(`select[name="career"]`);

                        careers.forEach(select => {
                            let selectedValue = select.value;
                            const careerid = select.dataset.careerid;
                            const assessor = select.dataset.assessor;


                            if (careerid && assessor && !select.disabled && selectedValue) { //เช็คว่ามี kpicode
                                const rowData = {
                                    cardid: cardid,
                                    dtl_id: kpiformdtl_id,
                                    type: type,
                                    assessor: assessor,
                                    //----------
                                    careerid: careerid,
                                    value: selectedValue,
                                };

                                payload.push(rowData);
                            }
                        });
                        break;


                }

            });


            //console.log(`Next Card`);
        }
    });
    console.log(JSON.stringify(payload)); // Shows payload in console

    return JSON.stringify(payload);
}
function fetching(ans) {
    console.log(ans);

    const json = JSON.parse(ans)
    console.log(json);

    if (json) {

        //console.log(json.Table[0].res_id);
        //console.log(json);
        //console.log(json.Table5);
        //console.log(json.Table5.length);
        //alert(json);


        if (json.Table1.length > 0) setCboCareer(json.Table1)
        if (json.Table2.length > 0) setCbo(json.Table2)
        if (json.Table3.length > 0) setCboKPIs(json.Table3)
        if (json.Table4.length > 0) setTextArea(json.Table4)
        if (json.Table5.length > 0) setProject(json.Table5)



        if ((json.Table1.length > 0) ||
            (json.Table2.length > 0) ||
            (json.Table3.length > 0) ||
            (json.Table4.length > 0) ||
            (json.Table5.length > 0)) {
            var form = $("#form1")[0];
            if (form.checkValidity() === false) {
                form.classList.add('was-validated');
            }
        }

    }
}
function setCboCareer(Table1) {
    const tableData = Table1; // ข้อมูลจาก Table2 ใน JSON

    tableData.forEach(item => {
        // ค้นหา div ที่มี data-rowdtlid ตรงกับ KPIFormDtl_ID1 ใน JSON
        const rowElement = document.querySelector(`div[data-rowdtlid="${item.KPIFormDtl_ID}"]`);

        if (rowElement) {
            const selector = rowElement.querySelector(`select[data-careerid="${item.career_id}"][data-assessor="${item.UserType}"]`);

            if (selector) {
                selector.value = item.txtRate
            }
        }
    });
}
function setCbo(Table2) {
    const tableData = Table2; // ข้อมูลจาก Table2 ใน JSON

    tableData.forEach(item => {
        // ค้นหา div ที่มี data-rowdtlid ตรงกับ KPIFormDtl_ID1 ใน JSON
        const cboElement = document.querySelector(`div[data-rowdtlid="${item.KPIFormDtl_ID}"][data-type="cbo"]`);

        if (cboElement) {
            const selector = cboElement.querySelector(`select`);

            if (selector) {
                selector.value = item.cbo_id
            }
        }

        const cboGroupElement = document.querySelector(`div[data-rowdtlid="${item.KPIFormDtl_ID}"][data-type="cboGroup"]`);

        if (cboGroupElement) {
            const groupCbo = cboGroupElement.querySelector(`select.groupCbo`);
            const subGroupCbo = cboGroupElement.querySelector(`select.subGroupCbo`);

            if (groupCbo) {
                groupCbo.value = item.cbo_Group
                updateSubGroupCbo(item.KPIFormDtl_ID, item.cbo_Group);
                if (subGroupCbo) {
                    subGroupCbo.value = item.cbo_subText
                    updateDetailCbo(item.KPIFormDtl_ID, item.cbo_Group, item.cbo_subText)
                }

            }

            const detailCbo = cboGroupElement.querySelector(`select.detailCbo`);

            if (detailCbo) {
                detailCbo.value = item.cbo_id
            }
        }

    });
}
function setCboKPIs(Table3) {
    const tableData = Table3; // ข้อมูลจาก Table2 ใน JSON
    tableData.forEach(item => {
        // ค้นหา div ที่มี data-rowdtlid ตรงกับ KPIFormDtl_ID1 ใน JSON
        const rowElement = document.querySelector(`div[data-rowdtlid="${item.KPIFormDtl_ID}"]`);

        if (rowElement) {
            const selector = rowElement.querySelector(`select[data-kpicode="${item.kpi_code}"][data-assessor="${item.UserType}"]`);
            const textarea = rowElement.querySelector(`textarea[data-kpicode="${item.kpi_code}"][data-assessor="${item.UserType}"]`);

            if (selector) {
                selector.value = item.txtRate
            }
            if (textarea) {
                textarea.value = item.txtReason || ''; // ถ้าไม่มีค่า txt_detail จะตั้งเป็นค่าว่าง
            }
        }
    });
}
function setTextArea(Table4) {
    const tableData = Table4; // ข้อมูลจาก Table4 ใน JSON

    tableData.forEach(item => {
        // ค้นหา div ที่มี data-rowdtlid ตรงกับ KPIFormDtl_ID1 ใน JSON
        const rowElement = document.querySelector(`div[data-rowdtlid="${item.KPIFormDtl_ID}"]`);

        if (rowElement) {
            // ค้นหา textarea ภายใน div ที่เจอ
            const textarea = rowElement.querySelector('textarea');

            if (textarea) {
                // ตั้งค่าให้กับ textarea ให้เท่ากับค่า txt_detail จาก JSON
                textarea.value = item.txt_detail || ''; // ถ้าไม่มีค่า txt_detail จะตั้งเป็นค่าว่าง
            }
        }
    });
}
function setProject(Table5) {
    const tableData = Table5; // ข้อมูลจาก Table5 ใน JSON
    //console.log(tableData)

    // ตรวจสอบว่ามีข้อมูลใน JSON หรือไม่
    if (tableData.length > 0) {
        //console.log(`innnnnnnnn`)
        const uniqueKPIFormDtlIDs = [...new Set(tableData.map(item => item.KPIFormDtl_ID))];
        uniqueKPIFormDtlIDs.map((KPIFormDtl_ID) => {
            // ค้นหา tbody โดยใช้ KPIFormDtl_ID จากข้อมูล JSON
            const tableBody = document.getElementById(KPIFormDtl_ID);

            if (tableBody && tableBody.rows.length > 0) {
                const tableDataByDtlId = tableData.filter((data) => data.KPIFormDtl_ID === KPIFormDtl_ID);

                //ลบค่าจาก tbody
                while (tableBody.firstChild) {
                    tableBody.removeChild(tableBody.firstChild);
                }

                // เพิ่มแถวใหม่สำหรับข้อมูลถัดไปใน Table5 (ถ้ามี)
                for (let i = 0; i < tableDataByDtlId.length; i++) {
                    const newRow = document.createElement('tr');

                    newRow.setAttribute("data-project_ans_id", `${tableDataByDtlId[i].project_ans_id || ''}`);
                    newRow.setAttribute("data-status", `${tableDataByDtlId[i].status || ''}`);
                    newRow.setAttribute("data-Approval_rate", `${tableDataByDtlId[i].Approval_rate || ''}`);
                    newRow.innerHTML = `<td class="text-center align-middle"><textarea class="form-control bg-transparent">${tableDataByDtlId[i].txtTopic || ''}</textarea></td>
                                        <td class="text-center align-middle"><textarea class="form-control bg-transparent">${tableDataByDtlId[i].txtObjective || ''}</textarea></td>
                                        <td class="text-center align-middle"><textarea class="form-control bg-transparent">${tableDataByDtlId[i].txtMethod || ''}</textarea></td>
                                        <td class="text-center align-middle"><textarea class="form-control bg-transparent">${tableDataByDtlId[i].txtResult || ''}</textarea></td>
                                        <td class="text-center align-middle"><textarea class="form-control bg-transparent">${tableDataByDtlId[i].txtStakeholders || ''}</textarea></td>
                                        <td class="text-center align-middle">
                                        <button onclick="deleteRow(this)" class="btn btn-link text-danger"><i class="fas fa-trash-alt"></i></button>
                                        </td>`;
                    tableBody.appendChild(newRow);
                }
            }

        });
    }
}

function addRadioToProject(assessor) {

    //alert('in updateTablesForApprover');
    // เช็คว่า assessor เป็น approver หรือไม่
    if (assessor === "approver") {
        // ค้นหาตารางทั้งหมดที่มีคลาส project__table
        const tables = document.querySelectorAll("table.project__table");

        // วนลูปผ่านตารางทั้งหมด
        tables.forEach(table => {
            // ค้นหา tbody ทั้งหมดในแต่ละตาราง
            const tbody = table.querySelector("tbody[data-assessor='owner']");

            // วนลูปผ่านแถวใน tbody
            Array.from(tbody.rows).forEach(row => {

                // ค้นหาเซลล์สุดท้ายในแถว (ที่มีปุ่มลบ)
                const lastCell = row.cells[row.cells.length - 1];

                // เคลียร์เนื้อหาทั้งหมดในเซลล์นั้น
                lastCell.innerHTML = "";

                lastCell.className = "";

                lastCell.classList.add("align-middle");
                lastCell.classList.add("text-center");
                lastCell.classList.add("bg-approver");

                // สร้าง div สำหรับคะแนน 5, 4, 3, 2, 1
                const scoreDiv = document.createElement("div");
                scoreDiv.className = "score-options d-flex flex-row justify-content-around";

                // สร้างปุ่ม radio สำหรับแต่ละคะแนน
                [5, 4, 3, 2, 1].forEach(score => {
                    const scoreContainer = document.createElement("div");
                    scoreContainer.className = "custom-control custom-radio d-inline-block mr-2";

                    const scoreRadio = document.createElement("input");
                    scoreRadio.type = "radio";
                    scoreRadio.className = "custom-control-input";
                    scoreRadio.id = `score_${score}_${table.id || "table"}_${row.rowIndex}`;
                    scoreRadio.name = `action_${table.id || "table"}_${row.rowIndex}`;
                    scoreRadio.value = score;
                    scoreRadio.required = true;

                    const scoreLabel = document.createElement("label");
                    scoreLabel.className = "custom-control-label";
                    scoreLabel.htmlFor = scoreRadio.id;
                    scoreLabel.textContent = score.toString();

                    scoreContainer.appendChild(scoreRadio);
                    scoreContainer.appendChild(scoreLabel);
                    scoreDiv.appendChild(scoreContainer);
                });

                // สร้าง div สำหรับ invalid-feedback
                const invalidFeedback = document.createElement("div");
                invalidFeedback.className = "invalid-feedback";
                invalidFeedback.textContent = "(ต้องเลือกคะแนน)"; // ข้อความแจ้งเตือน

                // เพิ่ม scoreDiv และ invalid-feedback เข้าไปในเซลล์สุดท้าย
                if (row.cells[0]) {
                    const span = row.cells[0].querySelector("span.form-control-span");
                    if (span && span.innerText.trim() !== '') {
                        lastCell.appendChild(scoreDiv);
                        lastCell.appendChild(invalidFeedback);
                    }
                }
                // ตรวจสอบค่า Approval_rate
                const approval_rate = row.dataset.approval_rate; // อ่านค่า approval_rate
                if (approval_rate && [5, 4, 3, 2, 1].includes(Number(approval_rate))) {
                    // Set radio เป็น checked ตามค่า approval_rate
                    const selectedRadio = lastCell.querySelector(`input[value="${approval_rate}"]`);
                    if (selectedRadio) {
                        selectedRadio.checked = true;
                    }
                }
            });
        });
    }

}
function replaceCboGroup(assessor) {
    const tables = document.querySelectorAll("table.table__cboGroup");

    let previousTableId = null; // ใช้เก็บ tableId ก่อนหน้า
    let runningNumber = 0; // หมายเลขรันนิ่ง

    tables.forEach((table, index) => {
        const tableAssessor = table.getAttribute("data-assessor");
        const currentTableId = parseInt(table.getAttribute("data-table-id"), 10);

        if (assessor === "approver" && tableAssessor === "owner") {
            // ตรวจสอบว่าต้องรีเซ็ตหมายเลขรันนิ่งหรือไม่
            if (previousTableId === null || Math.abs(currentTableId - previousTableId) > 1) {
                runningNumber = 1; // รีเซ็ตหมายเลขรันนิ่งใหม่
            } else {
                runningNumber += 1; // เพิ่มหมายเลขรันนิ่ง
            }

            // อัปเดต tableId ก่อนหน้า
            previousTableId = currentTableId;

            // สร้าง span เพื่อแทน table
            const span = document.createElement("span");
            span.className = "kpi-span ml-3";
            span.dataset.tableId = currentTableId;

            // เพิ่มหมายเลขรันนิ่ง
            //const numberLabel = document.createElement("strong");
            //numberLabel.textContent = `รายการที่: ${runningNumber}`;
            //span.appendChild(numberLabel);


            const divElem = document.createElement("div");
            divElem.className = `bg-${tableAssessor} p-2 align-items-center d-flex`;

            // ค้นหา select และแทนที่ด้วยค่า
            const selects = table.querySelectorAll("select");
            selects.forEach(select => {
                const selectedOption = select.options[select.selectedIndex];
                const selectedValue = selectedOption ? selectedOption.value.trim() : "";
                const selectedText = selectedOption ? selectedOption.textContent : "";
                const valueSpan = document.createElement("span");

                valueSpan.textContent = selectedValue ? selectedText : "-"; // ถ้ามีค่าให้ใส่ข้อความ
                valueSpan.className = "form-control-span";
                span.appendChild(valueSpan);
                if (select.className.includes('detailCbo')) span.appendChild(document.createElement("br"));
            });
            divElem.appendChild(span)

            // แทนที่ table ด้วย span
            table.parentNode.replaceChild(divElem, table);
        } else if (assessor === "owner" && tableAssessor === "approver") {
            //alert(assessor)
            // เงื่อนไขสำหรับ owner: ลบ table ที่ data-assessor="approver"
            //table.parentNode.removeChild(table);

            const divElem = document.createElement("div");
            divElem.className = `bg-${tableAssessor} p-2 align-items-center d-flex`;

            const span = document.createElement("span");
            span.className = "kpi-span ml-3";
            span.dataset.tableId = currentTableId;

            const txtSpan = document.createElement("span");
            txtSpan.textContent = "-"; // ถ้ามีค่าให้ใส่ข้อความ
            txtSpan.className = "form-control-span";
            span.appendChild(txtSpan);
            span.appendChild(document.createElement("br"));

            divElem.appendChild(span)

            table.parentNode.replaceChild(divElem, table);
        }
    });
}
function replaceCbo(assessor) {
    // เลือก div.row ที่มี data-type="cbo"
    const rows = document.querySelectorAll("div.row[data-type='cbo']");



    rows.forEach(row => {
        // ค้นหา select ภายใน row
        const select = row.querySelector("select.form-control");
        if (select) {
            const selectAssessor = select.getAttribute("data-assessor");
            const is_approveronly = select.getAttribute("data-label");

            // ตรวจสอบเงื่อนไข data-assessor="approver" และ assessor="owner
            if (selectAssessor === "approver" && assessor === "owner") {
                // ค้นหา div.col ที่เป็น parent ของ select
                const colDiv = select.closest(".col");
                if (colDiv) {
                    // ลบ child ทั้งหมดใน div.col
                    while (colDiv.firstChild) {
                        colDiv.removeChild(colDiv.firstChild);
                    }

                    if (!(is_approveronly === "พิจารณาโดยหัวหน้างาน")) {
                        // ลบ class mb-3 ออกจาก div.col
                        colDiv.classList.remove("mb-3");

                        // สร้าง div ใหม่เพื่อแทนที่ select
                        const divElem = document.createElement("div");
                        divElem.className = `bg-${selectAssessor} p-2 align-items-center d-flex`;

                        // สร้าง span ใหม่
                        const span = document.createElement("span");
                        span.className = "kpi-span ml-3";

                        // ตรวจสอบ selected value
                        const selectedOption = select.options[select.selectedIndex];
                        const selectedValue = selectedOption ? selectedOption.value.trim() : null;
                        const selectedText = selectedOption ? selectedOption.textContent : "-";

                        // สร้าง span สำหรับแสดงข้อความ
                        const txtSpan = document.createElement("span");
                        txtSpan.textContent = selectedValue ? selectedText : "-"; // ถ้าไม่มีค่าให้ใส่ "-"
                        txtSpan.className = "form-control-span";
                        span.appendChild(txtSpan);

                        // หากต้องการแยกบรรทัดในบางกรณี (เช่น className 'detailCbo')
                        if (select.className.includes("detailCbo")) {
                            span.appendChild(document.createElement("br"));
                        }

                        divElem.appendChild(span);

                        // เพิ่ม div ใหม่เข้าไปใน div.col
                        colDiv.appendChild(divElem);
                    }
                }
            } else if (selectAssessor === "owner" && assessor === "approver") {
                const colDiv = select.closest(".col");
                if (colDiv) {
                    // ลบ child ทั้งหมดใน div.col
                    while (colDiv.firstChild) {
                        colDiv.removeChild(colDiv.firstChild);
                    }

                    // ลบ class mb-3 ออกจาก div.col
                    colDiv.classList.remove("mb-3");

                    // สร้าง div ใหม่เพื่อแทนที่ select
                    const divElem = document.createElement("div");
                    divElem.className = `bg-${selectAssessor} p-2 align-items-center d-flex`;

                    // สร้าง span ใหม่
                    const span = document.createElement("span");
                    span.className = "kpi-span ml-3";

                    // ตรวจสอบ selected value
                    const selectedOption = select.options[select.selectedIndex];
                    const selectedValue = selectedOption ? selectedOption.value.trim() : null;
                    const selectedText = selectedOption ? selectedOption.textContent : "-";

                    // สร้าง span สำหรับแสดงข้อความ
                    const txtSpan = document.createElement("span");
                    txtSpan.textContent = selectedValue ? selectedText : "-"; // ถ้าไม่มีค่าให้ใส่ "-"
                    txtSpan.className = "form-control-span";
                    span.appendChild(txtSpan);
                    span.appendChild(document.createElement("br"));

                    divElem.appendChild(span);

                    // เพิ่ม div ใหม่เข้าไปใน div.col
                    colDiv.appendChild(divElem);
                }

            }
        }
    });
}
function replaceCell(assessor) {
    // เช็คว่า assessor เป็น approver หรือไม่
    if (assessor === "approver") {
        // ค้นหาตารางทั้งหมดที่มีคลาส project__table
        const tables = document.querySelectorAll("table");

        // วนลูปผ่านตารางทั้งหมด
        tables.forEach(table => {
            // ค้นหา tbody ทั้งหมดในแต่ละตาราง
            const tbody = table.querySelector("tbody");

            // วนลูปผ่านแถวใน tbody
            Array.from(tbody.rows).forEach(row => {
                // แทนที่ textarea ด้วย span ในแต่ละเซลล์ของแถว
                Array.from(row.cells).forEach(cell => {
                    const textarea = cell.querySelector("tbody[data-assessor='owner'] textarea,textarea[data-assessor='owner']");
                    if (textarea) {

                        cell.classList.remove("text-center");
                        // เพิ่ม class ใหม่เป็น text-left
                        cell.classList.add("text-left");
                        cell.classList.add("bg-owner");


                        // สร้าง span ใหม่และแทนที่ textarea
                        const span = document.createElement("span");
                        span.textContent = textarea.value || ""; // ใช้ค่าจาก textarea
                        span.className = "form-control-span prewrap"; // เพิ่ม class ถ้าต้องการ
                        cell.replaceChild(span, textarea); // แทนที่ textarea ด้วย span
                    }

                    // ตรวจหา select ที่อยู่ภายใน Bootstrap Select Plugin
                    const dropdownWrapper = cell.querySelector(".dropdown.bootstrap-select");
                    if (dropdownWrapper) {
                        const select = dropdownWrapper.querySelector("select[data-assessor='owner']");
                        if (select) {
                            // ดึงค่าที่เลือกจาก select หรือค่าที่แสดงในปุ่ม dropdown
                            const selectedOption = select.options[select.selectedIndex];
                            const selectedText = selectedOption ? selectedOption.textContent : "";

                            // สร้าง span เพื่อแทน dropdown
                            const span = document.createElement("span");
                            span.textContent = selectedText;
                            span.className = "form-control-span";

                            // ลบ dropdown wrapper และแทนที่ด้วย span
                            cell.replaceChild(span, dropdownWrapper);
                        }
                    }
                });
            });
        });
    }
}


function checkAndReplaceTablesProject(assessor) {
    if (assessor === "approver") {
        // ค้นหา table ทั้งหมดที่มีคลาส project__table
        const tables = document.querySelectorAll("table.project__table");

        tables.forEach((table) => {
            const tbody = table.querySelector("tbody");
            if (tbody) {
                const rows = tbody.querySelectorAll("tr");
                if (rows.length === 1) { // ตรวจสอบว่ามีเพียงแถวเดียวใน tbody
                    const firstCell = rows[0].querySelector("td:first-child");
                    const textarea = firstCell?.querySelector("textarea");

                    // ตรวจสอบว่า textarea ว่างเปล่า
                    if (textarea && textarea.value.trim() === "") {
                        const divElem = document.createElement("div");
                        divElem.className = "bg-light p-2 align-items-center d-flex";


                        // สร้าง span แทนที่ table
                        const span = document.createElement("span");
                        span.className = "kpi-span ml-3";
                        span.textContent = "-";

                        divElem.appendChild(span);

                        // แทนที่ table ด้วย span
                        table.replaceWith(divElem);
                    }
                }
            }
        });
    }
}

