import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageProvider {
  constructor() { }

  public setStorage(isPersistentStorage: boolean, storageKey: string, storageItem: any): void {
    if (isPersistentStorage) {
      localStorage.setItem(storageKey, JSON.stringify(storageItem));
    } else {
      sessionStorage.setItem(storageKey, JSON.stringify(storageItem));
    }
  }

  public getStorage(storageKey: string): any {
    let value = localStorage.getItem(storageKey);
    if (value == null) {
      value = sessionStorage.getItem(storageKey);
    }
    return JSON.parse(value as any);
  }

  public removeStorage(storageKey: string) {
    localStorage.removeItem(storageKey);
    sessionStorage.removeItem(storageKey);
  }

  public clearStorage() {
    localStorage.clear();
    sessionStorage.clear();
  }
}
