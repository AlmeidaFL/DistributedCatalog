export class EncryptionUtil {
  private static secretKey: Uint8Array = new TextEncoder().encode('/qCpYa/Ka+sk2LVGMYy9nx4sw51lAtMHIMqetPeJH34='.padEnd(32, ' ')); // 32 bytes
  private static iv: Uint8Array = new Uint8Array(16); // 16 bytes de zeros

  static async encrypt(text: string): Promise<string> {
      const algorithm = { name: 'AES-CBC', iv: this.iv };
      const key = await crypto.subtle.importKey(
          'raw', 
          this.secretKey, 
          algorithm, 
          false, 
          ['encrypt']
      );

      const encodedText = new TextEncoder().encode(text);
      const encrypted = await crypto.subtle.encrypt(algorithm, key, encodedText);

      return btoa(String.fromCharCode(...new Uint8Array(encrypted))); // Convertendo para Base64
  }

  static async decrypt(encryptedText: string): Promise<string> {
      const algorithm = { name: 'AES-CBC', iv: this.iv };
      const key = await crypto.subtle.importKey(
          'raw', 
          this.secretKey, 
          algorithm, 
          false, 
          ['decrypt']
      );

      const encryptedData = new Uint8Array(atob(encryptedText).split('').map(c => c.charCodeAt(0)));
      const decrypted = await crypto.subtle.decrypt(algorithm, key, encryptedData);

      return new TextDecoder().decode(decrypted);
  }
}
