import { Editor } from 'primereact/editor';
import { useState } from 'react';
import { FileUpload } from 'primereact/fileupload';
import './Step.css';
const Step = ({steps, stepId, updateStepInParentState}) => {
    const header = (
        <div>
            <span class="ql-formats">
              <button class="ql-bold"></button>
              <button class="ql-italic"></button>
              <button class="ql-underline"></button>
              <button class="ql-strike"></button>
            </span>
            <span class="ql-formats">
              <select class="ql-color"></select>
              <select class="ql-background"></select>
            </span>
            <span class="ql-formats">
              <button class="ql-script" value="sub"></button>
              <button class="ql-script" value="super"></button>
            </span>
            <span class="ql-formats">
              <button class="ql-blockquote"></button>
            </span>
            <span class="ql-formats">
              <button class="ql-list" value="ordered"></button>
              <button class="ql-list" value="bullet"></button>
              <button class="ql-indent" value="-1"></button>
              <button class="ql-indent" value="+1"></button>
            </span>
            <span class="ql-formats">
            <button class="ql-direction" value="rtl"></button>
            <select class="ql-align"></select>
            </span>
        </div>
        );

    const [description, setDescription] = useState('');
    const [image, setImage] = useState(null)
    const [imageURL, setImageURL] = useState(null)

    function descriptionChangeHandler(value){
        let newStep ={
            stepId:stepId,
            description:value,
            file:image,
        }
        setDescription(value)
        updateStepInParentState(newStep)
    }

    const onSelect = (event) => {
        let files = event.originalEvent.target.files
        let stepNode = document.querySelector(`#${stepId}`)
        if (files && files[0]) {
            let newStep ={
                stepId:stepId,
                description:description,
                file:files[0],
            }    
            setImage(files[0])
            setImageURL(URL.createObjectURL(files[0]));
            updateStepInParentState(newStep)
            let previewImage = stepNode.querySelector(".preview-image")
            previewImage.style.display = 'inline';
        }
    }

    function onClear(e){
        let newStep ={
            stepId:stepId,
            description:description,
            file:null,
        }    
        setImage(null)
        setImageURL(null)
        updateStepInParentState(newStep)

        let stepNode = document.querySelector(`#${stepId}`)
        let previewImage = stepNode.querySelector(".preview-image")
        previewImage.style.display = 'none';
    }
    return (
        <div id={stepId} className="step">
            <div>
                <table style={{margin:'0 auto'}}>
                    <tbody>
                        <tr>
                            <td>
                                <Editor value={description}  onTextChange={(e) => descriptionChangeHandler(e.htmlValue)} headerTemplate={header} style={{ height: '350px', width: '700px'}} className='step-description'/>
                            </td>
                            <td valign='top' textAlign='center' style={{valign:'top', textAlign:'center', minWidth:'600px', marginTop:'40px'}}>
                                <FileUpload mode="basic" chooseLabel='Добавить файл' onClear={onClear} onSelect={onSelect}  name="demo[]" accept="image/*" maxFileSize={1000000} style={{fontSize:'16px', margin:'0 auto', maxHeight:'100px'}}/>
                                <img alt="preview image" src={imageURL} style={{display:'none', textAlign:'left', maxWidth:'600px', maxHeight:'350px', margin:'30px auto 0 auto'}} className='preview-image'/>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
            </div>
        </div>
        );
}

export default Step;