import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CryptoService {

constructor() { }

encryptAPI(value) {
  let keys = CryptoJS.enc.Utf8.parse(environment.encryptionKey);
  let iv = CryptoJS.enc.Utf8.parse(environment.encryptioniv);



  var ciphertext = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(value), keys, {
    keySize: 128 / 8,
    iv: iv,
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7
  });

  return ciphertext.toString();
}

//The set method is use for encrypt the value.
encrypt(value){
  let keys = environment.encryptionKey;
  let iv= environment.encryptioniv;

  

  var ciphertext = CryptoJS.AES.encrypt(JSON.stringify(value), keys);

  return ciphertext.toString();
}

//The get method is use for decrypt the value.
decrypt(value){
  let keys = environment.encryptionKey;
  let iv= environment.encryptioniv;
  

  var bytes  = CryptoJS.AES.decrypt(value.toString(), keys);
  var decryptedData = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
  return decryptedData;
}
}
